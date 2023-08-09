using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseDL.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdayCvler",
                columns: table => new
                {
                    CvNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CvOlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OgrenimDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OkulAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bolum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OgrenimBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OgrenimBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsYeriAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDetayi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdayCvler", x => x.CvNo);
                });

            migrationBuilder.CreateTable(
                name: "Siciller",
                columns: table => new
                {
                    SicilNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siciller", x => x.SicilNo);
                });

            migrationBuilder.CreateTable(
                name: "SicilOgrenimler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SicilNo = table.Column<int>(type: "int", nullable: false),
                    OgrenimDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OkulAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OgrenimBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OgrenimBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SicilOgrenimler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SicilOgrenimler_Siciller_SicilNo",
                        column: x => x.SicilNo,
                        principalTable: "Siciller",
                        principalColumn: "SicilNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SicilUcretler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SicilNo = table.Column<int>(type: "int", nullable: false),
                    UcretTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UcretTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GecerlilikBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SicilUcretler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SicilUcretler_Siciller_SicilNo",
                        column: x => x.SicilNo,
                        principalTable: "Siciller",
                        principalColumn: "SicilNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SicilOgrenimler_SicilNo",
                table: "SicilOgrenimler",
                column: "SicilNo");

            migrationBuilder.CreateIndex(
                name: "IX_SicilUcretler_SicilNo",
                table: "SicilUcretler",
                column: "SicilNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdayCvler");

            migrationBuilder.DropTable(
                name: "SicilOgrenimler");

            migrationBuilder.DropTable(
                name: "SicilUcretler");

            migrationBuilder.DropTable(
                name: "Siciller");
        }
    }
}
