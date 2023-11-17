using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabloAdiBelirleme.Migrations
{
    public partial class mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Fiyat",
                table: "Urunler",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "UrunAdi",
                table: "Urunler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fiyat",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "UrunAdi",
                table: "Urunler");
        }
    }
}
