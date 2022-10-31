

using System.ComponentModel;
using System.Runtime.Serialization;

namespace PVI.GQKN.Infrastructure.Contracts;

[DataContract]
public class PagedListQueryParams
{

    /// <summary>
    /// Số phần tử / trang
    /// </summary>
    [DefaultValue(10)]
    [DataMember]
    public int PageSize { get; set; }


    /// <summary>
    /// Mã trang load. 0 nếu là trang đầu tiên
    /// </summary>
    [DataMember]
    public int? PageId { get; set; }
}
