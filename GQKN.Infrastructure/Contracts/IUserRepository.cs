using System.Runtime.Serialization;

namespace PVI.GQKN.Infrastructure.Contracts;

[DataContract]
public class UserParams: PagedListQueryParams
{
    /// <summary>
    /// Keyword: username, họ tên, địa chỉ, email, phone, mã user (mã PVI)
    /// </summary>
    [DataMember]
    public string SearchString { get; set; }

}

public interface IUserRepository: 
    IPageListRepository<ApplicationUser>
{

}
