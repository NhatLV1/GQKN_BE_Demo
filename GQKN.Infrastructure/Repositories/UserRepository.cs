using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.Infrastructure.Repositories;

public class UserRepository :
    RepositoryBase<ApplicationUser>,
    IUserRepository
{
    private readonly UserManager<ApplicationUser> userManager;

    public UserRepository(GQKNDbContext context,
        UserManager<ApplicationUser> userManager) : base(context)
    {
        this.userManager = userManager;
    }


    public override bool DeleteByGuid(string guid)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> DeleteByGuidAsync(string guid)
    {
        throw new NotImplementedException();
    }

    public override Task<ApplicationUser> GetByGuidAsync(string guid)
    {
        return this.userManager.FindByIdAsync(guid);
    }

    public async Task<PagedList<ApplicationUser>> GetDanhSachNguoiDung(UserParams request, string includeProperties = "")
    {
        var source = this.GetAll();
        var pageId = request.PageId ?? 0;

        source = ApplyQuery(request, ref source)
            .Where(b => b.UserId > pageId);

        source = IncludeProperties(source, includeProperties);

        var count = await source.CountAsync();

        var items = await source
            .Take(request.PageSize).ToListAsync();

        int? id = items.Count == request.PageSize ? items.Last().UserId : null;

        return new PagedList<ApplicationUser>(items, count, request.PageSize, request.PageId, id);
    }

    public Task<PagedList<ApplicationUser>> GetPage(PagedListQueryParams request, string includeProperties = "")
    {
        return GetDanhSachNguoiDung((UserParams)request, includeProperties);
    }

    public IQueryable<ApplicationUser> ApplyQuery(PagedListQueryParams r,
        ref IQueryable<ApplicationUser> q)
    {
        return this.ApplyQuery((UserParams)r, ref q);
    }

    private IQueryable<ApplicationUser> ApplyQuery(UserParams r,
        ref IQueryable<ApplicationUser> q)
    {
        if (!string.IsNullOrEmpty(r.SearchString))
        {
            q = q.Where(e => e.UserName.Contains(r.SearchString)
                 || e.HoTen.Contains(r.SearchString)
                 || e.DiaChi.Contains(r.SearchString)
                 || e.Email.Contains(r.SearchString)
                 || e.PhoneNumber.Contains(r.SearchString)
                 || e.MaUserPVI.Contains(r.SearchString));
        }


        return q;
    }

}
