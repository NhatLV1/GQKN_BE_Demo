using Microsoft.AspNetCore.Mvc;
using PVI.GQKN.API.Application.File;
using PVI.GQKN.Infrastructure.Extensions;
using System.Net.Http;

namespace PVI.GQKN.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly PiasCredentials _piasCredential;
        private readonly HttpClient _apiClient;
        private readonly string _rootPath;
        private const int _maxFileSize = 5 * 1024 * 1024;
        private readonly GQKNDbContext _context;
        private readonly string _upLoadUrl;
        private readonly string _downLoadUrl;

        public FileController(HttpClient httpClien, IWebHostEnvironment hostingEnvironment, GQKNDbContext context)
        {
            _apiClient = httpClien;
            _rootPath = $"{hostingEnvironment.WebRootPath}{hostingEnvironment.ContentRootPath}";
            _context = context;
            _upLoadUrl = $"{_piasCredential.Url}/API_App1/ManagerApplication/Upload_File_CP";
            _downLoadUrl = $"{_piasCredential.Url}/API_App1/ManagerApplication/DownloadFile_CP";
        }

        [Route("upload-file")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UploadFileBookMark([FromForm] TaiLieuRequest requestFile)
        {
            string msgResult = "";
            try
            {
                if (requestFile.File != null)
                {
                    var typeFormat = requestFile.File.FileName.Split(".")?.Last();
                    if (!string.IsNullOrEmpty(typeFormat))
                    {
                        var fileName = DateTime.Now.Ticks.ToString() + "." + typeFormat;
                        var folder = "FileUpload/";
                        var pathForder = Path.Combine(_rootPath, folder);
                        if (!Directory.Exists(pathForder))
                        {
                            Directory.CreateDirectory(pathForder);
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            await requestFile.File.CopyToAsync(memoryStream);

                            if (memoryStream.Length > _maxFileSize)
                            {
                                msgResult = $"Maximum allowed file size is {_maxFileSize} bytes.";
                                return Ok(new { check = false, content = msgResult });
                            }

                            var filePath = Path.Combine(pathForder, fileName);
                            var result = folder + fileName;
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                await requestFile.File.CopyToAsync(stream);

                                #region push file to PVI
                                // convert file to base64
                                var base64file = StreamExtensions.ConvertToBase64(stream);

                                var secret = $"{_piasCredential.Key}{typeFormat}{result}";

                                var md5Secret = secret.MD5Hash().ToLower();

                                UploadFile_Content content = new UploadFile_Content()
                                {
                                    CpId = _piasCredential.CpId,
                                    Sign = md5Secret,
                                    file_size = base64file,
                                    file_extension = typeFormat,
                                    path = result
                                };

                                var json = JsonSerializer.Serialize(content);
                                var loginContent = new StringContent(json,
                                    Encoding.UTF8, "application/json");

                                var response = await _apiClient.PostAsync(_upLoadUrl, loginContent);

                                response.EnsureSuccessStatusCode();

                                var jsonResponse = await response.Content.ReadAsStringAsync();

                                UploadFile_Result uploadFile_Result = JsonSerializer.Deserialize<UploadFile_Result>(jsonResponse, new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                });
                                #endregion

                                //nếu push file to PVI thành công
                                if (uploadFile_Result.Status == "00")
                                {
                                    //save tai lieu
                                    TaiLieu tailieu = new TaiLieu();
                                    tailieu.HoSoTonThatId = requestFile.HoSoTonThatId;
                                    tailieu.ThuMucId = requestFile.ThuMucId;
                                    tailieu.TenTaiLieu = fileName;
                                    tailieu.DuongDan = result;
                                    _context.TaiLieu.Add(tailieu);

                                    await _context.SaveChangesAsync();

                                    //add bookmark
                                    if (requestFile.BookMarks.Count > 0)
                                    {
                                        List<TaiLieuBookMark> lstBm = new List<TaiLieuBookMark>();
                                        foreach (var item in requestFile.BookMarks)
                                        {
                                            var codeBookMark = GenerateHashCode.GenerateCodeBoookMarkFile();
                                            TaiLieuBookMark tl_bookmark = new TaiLieuBookMark();
                                            tl_bookmark.TaiLieuId = tailieu.Id;
                                            tl_bookmark.MaBookMark = codeBookMark;
                                            tl_bookmark.TenBookMark = item.TieuDe;
                                            tl_bookmark.DuongDanBookMark = $"{result}/{codeBookMark}";
                                            tl_bookmark.Note = item.GhiChu;
                                            lstBm.Add(tl_bookmark);
                                        }
                                        _context.TaiLieuBookMark.AddRange(lstBm);

                                        await _context.SaveChangesAsync();
                                    }

                                    return Ok(new { check = true, content = result });
                                }
                                else
                                {
                                    return Ok(new { check = false, content = "Save file PVI err" });
                                }

                            }
                        }
                    }
                    else
                    {
                        msgResult = $"fail! type format false";
                        return Ok(new { check = false, content = msgResult });
                    }
                }
                else
                {
                    msgResult = $"This file is null";
                    return Ok(new { check = false, content = msgResult });
                }
            }
            catch (Exception ex)
            {
                msgResult = ex.ToString();
                return Ok(new { check = false, content = msgResult });
            }
        }
    }
}
