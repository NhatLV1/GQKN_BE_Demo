namespace PVI.GQKN.API.Application.Commands.ThuMucCommands;

[DataContract]
public class UpdateThuMucCommand : IRequest<ThuMuc>
{
    internal string _Id { get; set; }

    [Required]
    [DataMember]
    public int HoSoTonThatId { get; set; }
    [Required]
    [DataMember]
    public string TenThuMuc { get; set; }
    [DataMember]
    public int TrangThai { get; set; }
}
