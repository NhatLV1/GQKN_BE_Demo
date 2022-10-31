namespace PVI.GQKN.API.Application.Commands.ChucDanhCommands
{

    public class CreateChucDanhCommand: IRequest<ChucDanh>
    {
        public string MaChucVu { get; set; }
        public string TenChucVu { get; set; }
    }
}
