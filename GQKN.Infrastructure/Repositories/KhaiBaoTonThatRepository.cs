namespace PVI.GQKN.Infrastructure.Repositories;

public class KhaiBaoTonThatRepository :
    RepositoryBaseEntity<KhaiBaoTonThat>, IKhaiBaoTonThatRepository
{
    public KhaiBaoTonThatRepository(GQKNDbContext context) : base(context)
    {

    }

    public override IQueryable<KhaiBaoTonThat> ApplyQuery(PagedListQueryParams @params, 
        ref IQueryable<KhaiBaoTonThat> q)
    {
        HSTTParams request = @params as HSTTParams;

        if (!string.IsNullOrEmpty(request.MaHoSo))
        {
            q = q.Where(e => e.MaDinhDanh.Contains(request.MaHoSo)); 
        }

        if (!string.IsNullOrEmpty(request.DonViGQKN))
        {
            //q = q.Where(e => e.DonViCapDon.TenDonVi.Contains(request.DonViGQKN));
            // TODO: 
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
            q = q.Where(e => e.SoDonBaoHiem.Contains(request.SoDon));
        }

        if (!string.IsNullOrEmpty(request.NguoiDuocBaoHiem))
        {
            q = q.Where(e => e.HoTen.Contains(request.NguoiDuocBaoHiem));
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
            q = q.Where(e => e.TrangThaiTaiLieu == request.TrangThaiTaiLieu);
        }

        return q;
    }
}
