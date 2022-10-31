namespace PVI.GQKN.API.Application.Dtos
{
    public class PhongBanInfo
    {
        public int Id { get; private set; }

        public string Guid { get; private set; }

        public string TenPhongBan { get; private set; }

        public int? DonViId { get; private set; }

        public PhongBanInfo(int id, string tenPhongBan, string guid, int? donViId)
        {
            Id = id;
            TenPhongBan = tenPhongBan;
            Guid = guid;
            DonViId = donViId;
        }
    }
}
