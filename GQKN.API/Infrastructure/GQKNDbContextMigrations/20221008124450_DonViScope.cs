using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVI.GQKN.API.Infrastructure.GQKNDbContextMigrations
{
    public partial class DonViScope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scope",
                columns: table => new
                {
                    DonViId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scope", x => new { x.DonViId, x.Id });
                    table.ForeignKey(
                        name: "FK_Scope_tbl_don_vi_DonViId",
                        column: x => x.DonViId,
                        principalTable: "tbl_don_vi",
                        principalColumn: "pr_key",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scope");
        }
    }
}
