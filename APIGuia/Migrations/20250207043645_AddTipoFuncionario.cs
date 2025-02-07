using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGuia.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoFuncionario",
                table: "Usuarios",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoFuncionario",
                table: "Usuarios");
        }
    }
}
