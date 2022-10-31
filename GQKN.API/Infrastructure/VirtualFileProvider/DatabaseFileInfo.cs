namespace PVI.GQKN.API.Infrastructure.VirtualFileProvider;

public class DatabaseFileInfo : IFileInfo
{
    private string _viewPath;
    private byte[] _viewContent;
    private DateTimeOffset _lastModified;
    private bool _exists;
    private BieuMau template;

    public DatabaseFileInfo(BieuMau template)
    {
        this.template = template;
        GetView();
    }

    public bool Exists => _exists;

    public bool IsDirectory => false;

    public DateTimeOffset LastModified => _lastModified;

    public long Length
    {
        get
        {
            using (var stream = new MemoryStream(_viewContent))
            {
                return stream.Length;
            }
        }
    }

    public string Name => Path.GetFileName(_viewPath);

    public string PhysicalPath => null;

    public Stream CreateReadStream()
    {
        return new MemoryStream(_viewContent);
    }

    private void GetView()
    {
        _exists = template != null;
        if (_exists)
        {
            _viewContent = Encoding.UTF8.GetBytes(template.NoiDung);
            _lastModified = template.NgaySua ?? DateTime.Now;
            template.NgayTruyXuat = DateTime.Now;
        }
    }
}