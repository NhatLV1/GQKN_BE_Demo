using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PVI.GQKN.Domain.Models;

public partial class DonVi: Entity, IAggregateRoot
{
    public string MaDonVi { get; set; }
    public string TenDonVi { get; set; }
    public string MaTinh { get; set; }

    public List<Scope> Scopes { get; private set; }


    public void AddScope(Scope s)
    {
        Scopes = Scopes ?? new List<Scope>();
        Scopes.Add(s);
    }
    public void AddScope(IEnumerable<Scope> s)
    {
        Scopes = Scopes ?? new List<Scope>();
        Scopes.AddRange(s);
    }

    public void ClearScopes()
    {
        if (this.Scopes != null)
            this.Scopes.Clear();
    }
}

public class Scope
{
    [MaxLength(50)]
    [Required]
    public string Name { get;  set; }
    [Required]
    [MaxLength(20)]
    public string Code { get;  set; }

    public Scope()
    {

    }
    public Scope(string name, string code)
    {
        Name = name;
        Code = code;
    }
}