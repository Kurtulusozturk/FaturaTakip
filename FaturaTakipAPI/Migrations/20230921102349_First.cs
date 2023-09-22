using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaturaTakipAPI.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SirketID",
                table: "Müsteriler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MusteriID",
                table: "Faturalar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SirketID",
                table: "Faturalar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Müsteriler_SirketID",
                table: "Müsteriler",
                column: "SirketID");

            migrationBuilder.CreateIndex(
                name: "IX_Faturalar_MusteriID",
                table: "Faturalar",
                column: "MusteriID");

            migrationBuilder.CreateIndex(
                name: "IX_Faturalar_SirketID",
                table: "Faturalar",
                column: "SirketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturalar_Müsteriler_MusteriID",
                table: "Faturalar",
                column: "MusteriID",
                principalTable: "Müsteriler",
                principalColumn: "MusteriID");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturalar_Sirketler_SirketID",
                table: "Faturalar",
                column: "SirketID",
                principalTable: "Sirketler",
                principalColumn: "SirketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Müsteriler_Sirketler_SirketID",
                table: "Müsteriler",
                column: "SirketID",
                principalTable: "Sirketler",
                principalColumn: "SirketID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturalar_Müsteriler_MusteriID",
                table: "Faturalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturalar_Sirketler_SirketID",
                table: "Faturalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Müsteriler_Sirketler_SirketID",
                table: "Müsteriler");

            migrationBuilder.DropIndex(
                name: "IX_Müsteriler_SirketID",
                table: "Müsteriler");

            migrationBuilder.DropIndex(
                name: "IX_Faturalar_MusteriID",
                table: "Faturalar");

            migrationBuilder.DropIndex(
                name: "IX_Faturalar_SirketID",
                table: "Faturalar");

            migrationBuilder.DropColumn(
                name: "SirketID",
                table: "Müsteriler");

            migrationBuilder.DropColumn(
                name: "MusteriID",
                table: "Faturalar");

            migrationBuilder.DropColumn(
                name: "SirketID",
                table: "Faturalar");
        }
    }
}
