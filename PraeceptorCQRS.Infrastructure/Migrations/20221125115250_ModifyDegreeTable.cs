using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraeceptorCQRS.Infrastructure.Migrations
{
    public partial class ModifyDegreeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LatoSensu",
                table: "PreceptorDegreeTypeTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StrictoSensu",
                table: "PreceptorDegreeTypeTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatoSensu",
                table: "PreceptorDegreeTypeTable");

            migrationBuilder.DropColumn(
                name: "StrictoSensu",
                table: "PreceptorDegreeTypeTable");
        }
    }
}
