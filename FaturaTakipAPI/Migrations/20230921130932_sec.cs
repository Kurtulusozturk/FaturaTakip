using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaturaTakipAPI.Migrations
{
    public partial class sec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturalar_Müsteriler_MusteriID",
                table: "Faturalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Müsteriler_Sirketler_SirketID",
                table: "Müsteriler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Müsteriler",
                table: "Müsteriler");

            migrationBuilder.RenameTable(
                name: "Müsteriler",
                newName: "Musteriler");

            migrationBuilder.RenameIndex(
                name: "IX_Müsteriler_SirketID",
                table: "Musteriler",
                newName: "IX_Musteriler_SirketID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler",
                column: "MusteriID");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturalar_Musteriler_MusteriID",
                table: "Faturalar",
                column: "MusteriID",
                principalTable: "Musteriler",
                principalColumn: "MusteriID");

            migrationBuilder.AddForeignKey(
                name: "FK_Musteriler_Sirketler_SirketID",
                table: "Musteriler",
                column: "SirketID",
                principalTable: "Sirketler",
                principalColumn: "SirketID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturalar_Musteriler_MusteriID",
                table: "Faturalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Musteriler_Sirketler_SirketID",
                table: "Musteriler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler");

            migrationBuilder.RenameTable(
                name: "Musteriler",
                newName: "Müsteriler");

            migrationBuilder.RenameIndex(
                name: "IX_Musteriler_SirketID",
                table: "Müsteriler",
                newName: "IX_Müsteriler_SirketID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Müsteriler",
                table: "Müsteriler",
                column: "MusteriID");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturalar_Müsteriler_MusteriID",
                table: "Faturalar",
                column: "MusteriID",
                principalTable: "Müsteriler",
                principalColumn: "MusteriID");

            migrationBuilder.AddForeignKey(
                name: "FK_Müsteriler_Sirketler_SirketID",
                table: "Müsteriler",
                column: "SirketID",
                principalTable: "Sirketler",
                principalColumn: "SirketID");
        }
    }
}
