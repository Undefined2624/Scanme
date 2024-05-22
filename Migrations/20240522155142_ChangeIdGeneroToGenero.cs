using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRMascotas.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdGeneroToGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdGenero",
                table: "Mascotas",
                newName: "Genero_temp");

            migrationBuilder.AlterColumn<bool>(
                name: "Genero_temp",
                table: "Mascotas",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.RenameColumn(
                name: "Genero_temp",
                table: "Mascotas",
                newName: "Genero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genero",
                table: "Mascotas",
                newName: "Genero_temp");

            migrationBuilder.AlterColumn<int>(
                name: "Genero_temp",
                table: "Mascotas",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.RenameColumn(
                name: "Genero_temp",
                table: "Mascotas",
                newName: "IdGenero");
        }
    }
}
