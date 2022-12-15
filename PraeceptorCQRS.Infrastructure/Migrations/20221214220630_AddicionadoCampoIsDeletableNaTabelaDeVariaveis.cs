using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraeceptorCQRS.Infrastructure.Migrations
{
    public partial class AddicionadoCampoIsDeletableNaTabelaDeVariaveis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeletable",
                table: "VariableXTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeletable",
                table: "VariableXTable");
        }
    }
}
