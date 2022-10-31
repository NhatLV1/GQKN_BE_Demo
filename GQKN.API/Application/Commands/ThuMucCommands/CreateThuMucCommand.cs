namespace PVI.GQKN.API.Application.Commands.ThuMucCommands;

[DataContract]
public class CreateThuMucCommand : IRequest<ThuMuc>
{
    [Required]
    [DataMember]
    public int HoSoTonThatId { get; set; }
    [Required]
    [DataMember]
    public string TenThuMuc { get; set; }
    [DataMember]
    public int TrangThai { get; set; }

}
