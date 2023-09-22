using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaturaTakipAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faturalar",
                columns: table => new
                {
                    FaturaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaturaNo = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: true),
                    FaturaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SiparisNo = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: true),
                    SiparisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Urun = table.Column<string>(type: "Varchar(100)", maxLength: 100, nullable: true),
                    Miktar = table.Column<int>(type: "int", nullable: true),
                    BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KDV = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KDVOrani = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OdenecekTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Durum = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturalar", x => x.FaturaID);
                });

            migrationBuilder.CreateTable(
                name: "Müsteriler",
                columns: table => new
                {
                    MusteriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: false),
                    Soyad = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: false),
                    Adres = table.Column<string>(type: "Varchar(100)", maxLength: 100, nullable: true),
                    Eposta = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: true),
                    TelefonNo = table.Column<string>(type: "Varchar(15)", maxLength: 15, nullable: true),
                    Durum = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Müsteriler", x => x.MusteriID);
                });

            migrationBuilder.CreateTable(
                name: "Sirketler",
                columns: table => new
                {
                    SirketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SirketAdı = table.Column<string>(type: "Varchar(100)", maxLength: 100, nullable: false),
                    Adres = table.Column<string>(type: "Varchar(100)", maxLength: 100, nullable: true),
                    TelefonNo = table.Column<string>(type: "Varchar(15)", maxLength: 15, nullable: true),
                    WebAdresi = table.Column<string>(type: "Varchar(200)", maxLength: 200, nullable: true),
                    Eposta = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                    VergiDairesi = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: true),
                    VergiKimlikNo = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: true),
                    Sifre = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sirketler", x => x.SirketID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faturalar");

            migrationBuilder.DropTable(
                name: "Müsteriler");

            migrationBuilder.DropTable(
                name: "Sirketler");
        }
    }
}
