using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRMascotas.Migrations
{
    /// <inheritdoc />
    public partial class AddImagenUrlToMascota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "Mascotas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "Mascotas");
        }
    }
}
