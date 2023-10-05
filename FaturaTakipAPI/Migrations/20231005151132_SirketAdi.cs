using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaturaTakipAPI.Migrations
{
    public partial class SirketAdi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SirketAdı",
                table: "Sirketler",
                newName: "SirketAdi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SirketAdi",
                table: "Sirketler",
                newName: "SirketAdı");
        }
    }
}
