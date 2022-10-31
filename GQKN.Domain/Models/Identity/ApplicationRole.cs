namespace PVI.GQKN.Domain.Models.Identity;

public class ApplicationRole: IdentityRole, IAggregateRoot
{
    //public int VaiTroId { get; set; }
    public DonVi DonVi { get; set; }

    public int? DonViId { get; set; }

    public int? PhongBanId { get; set; }

    public PhongBan PhongBan { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgaySua { get; set; }

    public ApplicationRole()
    {

    }

    public ApplicationRole(string name) : base(name)
    {

    }
}
