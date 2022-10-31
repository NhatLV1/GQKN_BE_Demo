using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVI.GQKN.Domain.Models;

public partial class TaiLieu : Entity, IAggregateRoot
{
    [Column("ma_kbtt")]
    public int HoSoTonThatId { get; set; }
    [Column("thu_muc_id")]
    public int ThuMucId { get; set; }
    [Column("ten_tai_lieu")]
    public string TenTaiLieu { get; set; }
    [Column("duong_dan")]
    public string DuongDan { get; set; }
    public HoSoTonThat HoSoTonThat { get; set; }
    public ThuMuc ThuMuc { get; set; }
}
