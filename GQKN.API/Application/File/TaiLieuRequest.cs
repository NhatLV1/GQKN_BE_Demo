namespace PVI.GQKN.API.Application.File
{
    public class TaiLieuRequest
    {
        public IFormFile File { get; set; }
        public int HoSoTonThatId { get; set; }
        public int ThuMucId { get; set; }
        public List<BookMarkNote> BookMarks { get; set; }
    }

    public class BookMarkNote
    {
        public string TieuDe { get; set; }
        public string GhiChu { get; set; }
    }

    public class UploadFile_Content
    {
        public string CpId { get; set; }
        public string Sign { get; set; }
        public string file_size { get; set; }
        public string file_extension { get; set; }
        public string path { get; set; }
    }

    public class DownloadFile_Content
    {
        public string FilePath { get; set; }
        public string CpId { get; set; }
        public string Sign { get; set; }
    }

    public class UploadFile_Result
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Path_Current { get; set; }
    }

    public class DownloadFile_Result
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }

}
