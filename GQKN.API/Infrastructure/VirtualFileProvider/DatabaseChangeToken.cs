using Microsoft.Extensions.Primitives;

namespace PVI.GQKN.API.Infrastructure.VirtualFileProvider;

public class DatabaseChangeToken : IChangeToken
{
    public DatabaseChangeToken(DatabaseFileProvider provider, int id)
    {
        templateId = id;
        Provider = provider;
    }

    public bool ActiveChangeCallbacks => false;
    
    public bool HasChanged
    {
        get
        {
            BieuMau template = Provider.Find(templateId);
            if (template == null)
                return false;

            var changed = template.NgayTruyXuat == null || template.NgaySua > template.NgayTruyXuat;
            return changed;
        }
    }

    private int templateId;

    public DatabaseFileProvider Provider { get; }

    public IDisposable RegisterChangeCallback(Action<object> callback, object state) => EmptyDisposable.Instance;
}

internal class EmptyDisposable : IDisposable
{
    public static EmptyDisposable Instance { get; } = new EmptyDisposable();
    private EmptyDisposable() { }
    public void Dispose() { }
}