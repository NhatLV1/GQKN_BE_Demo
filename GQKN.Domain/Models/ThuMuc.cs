using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVI.GQKN.Domain.Models;

public partial class ThuMuc : Entity, IAggregateRoot
{
    [Column("ma_kbtt")]
    public int HoSoTonThatId { get; set; }
    [Column("ten_thu_muc")]
    public string TenThuMuc { get; set; }
    [Column("trang_thai")]
    public int TrangThai { get; set; }
    public HoSoTonThat HoSoTonThat { get; set; }
}
