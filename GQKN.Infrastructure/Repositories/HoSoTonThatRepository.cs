using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.Infrastructure.Repositories;

public class HoSoTonThatRepository :
    RepositoryBaseEntity<HoSoTonThat>, IHoSoTonThatRepository
{
    public HoSoTonThatRepository(GQKNDbContext context) : base(context)
    {

    }

    public override IQueryable<HoSoTonThat> ApplyQuery(PagedListQueryParams @params, ref IQueryable<HoSoTonThat> q)
    {
        HSTTParams request = @params as HSTTParams;

        if (!string.IsNullOrEmpty(request.MaHoSo))
        {
            // TODO: 
        }

        if (!string.IsNullOrEmpty(request.DonViGQKN))
        {
            q = q.Where(e => e.DonViCapDon.TenDonVi.Contains(request.DonViGQKN));
        }

        if (!string.IsNullOrEmpty(request.TenHSBT))
        {
            // TODO: 
        }

        if (!string.IsNullOrEmpty(request.DonViCD))
        {
            q = q.Where(e => e.DonViCapDon.TenDonVi.Contains(request.DonViCD));
        }

        if (!string.IsNullOrEmpty(request.SoDon))
        {
            q = q.Where(e => e.DonBaoHiem.SoDon.Contains(request.SoDon));
        }

        if (!string.IsNullOrEmpty(request.NguoiDuocBaoHiem))
        {
            q = q.Where(e => e.NguoiDuocBaoHiem.HoTen.Contains(request.NguoiDuocBaoHiem));
        }

        if (request.NgayTonThat != null)
        {
            q = q.Where(e => e.ThoiGianTonThat == request.NgayTonThat);
        }

        if (request.SoTienYCBoiThuong != null)
        {
            q = q.Where(e => e.UocLuongTonThat == request.SoTienYCBoiThuong);
        }

        if (request.CongTyGiamDinh != null)
        { 
            // TODO
        }

        if(request.TrangThaiTaiLieu != null)
        {

        }

        return q;
    }
}
