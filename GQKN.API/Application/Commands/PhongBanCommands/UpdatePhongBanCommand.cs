namespace PVI.GQKN.API.Application.Commands.DonViCommands;

[DataContract]
public class UpdatePhongBanCommand: IRequest<PhongBan>
{
    internal string _Id { get; set; }

    /// <summary>
    /// Tên đơn vị
    /// </summary>
    [Required]
    [DataMember]
    public string TenPhongBan { get; set; }

    /// <summary>
    /// Mã đơn vị quản lý (optional)
    /// </summary>
    [DataMember]
    [DefaultValue(null)]
    public int? PhongBanChaId { get; set; }

    /// <summary>
    /// Mã đơn vị quản lý (optional) 
    ///  <br/>Ban = 1,
    ///  <br/>Phong = 2,
    /// </summary>
    [DataMember]
    public LoaiPhongBan LoaiPhongBan { get; set; }

    /// <summary>
    /// Mã đơn vị quản lý
    /// </summary>
    [Required]
    [DataMember]
    public int? DonViId { get; set; }

    /// <summary>
    /// Mã phòng ban
    /// </summary>
    [DataMember]
    public string MaPhongBan { get; set; }
}
