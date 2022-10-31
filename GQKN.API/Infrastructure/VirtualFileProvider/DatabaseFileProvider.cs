using Microsoft.Extensions.Primitives;
using PVI.GQKN.Infrastructure.Contracts;
using System.Collections;

namespace PVI.GQKN.API.Infrastructure.VirtualFileProvider;

public class DatabaseFileProvider : IFileProvider, IDirectoryContents
{
    private IBieuMauRepository _repository;
    private string _connection;
    private List<BieuMau> _templates;
    private readonly IServiceProvider _serviceProvider;

    public List<BieuMau> Templates
    {
        get
        {
            if (_templates == null)
            {
                FetchCacheTemplates();
            }
            return _templates;
        }
    }
    public bool Exists => false;

    public void FetchCacheTemplates()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<GQKNDbContext>();
            _repository = new BieuMauRepository(context);
            _templates = _repository.GetAll().ToList();
        }

    }

    public DatabaseFileProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        return this;
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        var filename = Path.GetFileNameWithoutExtension(subpath);
        var template = Templates.FirstOrDefault(e => e.TenBieuMau.Equals(filename));
        if (template == null)
            return new NotFoundFileInfo(subpath);
        var result = new DatabaseFileInfo(template);
        return result.Exists ? result as IFileInfo : new NotFoundFileInfo(subpath);
    }

    public IChangeToken Watch(string filter)
    {
        var filename = Path.GetFileNameWithoutExtension(filter);
        var template = Templates.FirstOrDefault(e => e.TenBieuMau.Equals(filename));
        if (template == null)
            return null;
        return new DatabaseChangeToken(this, template.Id);
    }

    public IEnumerator<IFileInfo> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    internal BieuMau Find(int id)
    {
        return Templates.FirstOrDefault(e => e.Id == id);
    }
}
