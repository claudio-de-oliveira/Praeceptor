using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraeceptorCQRS.Infrastructure.Migrations
{
    public partial class CampoCode3AdicionadoNasTabelasDeTipos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code3",
                table: "PreceptorRoleTypeTable",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code3",
                table: "PreceptorRegimeTypeTable",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code3",
                table: "PreceptorDegreeTypeTable",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code3",
                table: "ClassTypeTable",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code3",
                table: "PreceptorRoleTypeTable");

            migrationBuilder.DropColumn(
                name: "Code3",
                table: "PreceptorRegimeTypeTable");

            migrationBuilder.DropColumn(
                name: "Code3",
                table: "PreceptorDegreeTypeTable");

            migrationBuilder.DropColumn(
                name: "Code3",
                table: "ClassTypeTable");
        }
    }
}
