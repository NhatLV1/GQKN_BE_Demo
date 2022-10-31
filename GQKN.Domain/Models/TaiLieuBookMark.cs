using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVI.GQKN.Domain.Models;

public partial class TaiLieuBookMark: Entity, IAggregateRoot
{
    [Column("ma_tlieu")]
    public int TaiLieuId { get; set; }
    [Column("ten_bookmark")]
    public string TenBookMark { get; set; }
    [Column("ddan_bookmark")]
    public string DuongDanBookMark { get; set; }
    [Column("ma_bookmark")]
    public string MaBookMark { get; set; }
    [Column("note")]
    public string Note { get; set; }
    public TaiLieu TaiLieu { get; set; }
}
