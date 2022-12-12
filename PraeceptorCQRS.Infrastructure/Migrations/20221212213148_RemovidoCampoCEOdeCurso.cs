using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraeceptorCQRS.Infrastructure.Migrations
{
    public partial class RemovidoCampoCEOdeCurso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEO",
                table: "CourseTable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CEO",
                table: "CourseTable",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
