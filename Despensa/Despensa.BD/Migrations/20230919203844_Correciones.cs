using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Despensa.BD.Migrations
{
    /// <inheritdoc />
    public partial class Correciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Usuario_DNI_UQ",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "Usuario_DNI_UQ",
                table: "Usuarios",
                column: "DNI",
                unique: true);
        }
    }
}
