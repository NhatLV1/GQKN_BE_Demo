namespace PVI.GQKN.Domain.Models.Email;

public class EmailTemplate: Entity, IAggregateRoot
{
    public string TenTemplate { get; set; }
    public string TieuDe { get; set; }
    public string GuiTu { get; set; }
    public string EmailGui { get; set; }
}
