using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraeceptorCQRS.Infrastructure.Migrations
{
    public partial class SubstituicaoDasTabelasDeVariaveis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariableValueTable");

            migrationBuilder.DropTable(
                name: "GroupValueTable");

            migrationBuilder.DropTable(
                name: "VariableTable");

            migrationBuilder.DropTable(
                name: "GroupTable");

            migrationBuilder.AlterColumn<int>(
                name: "Curriculum",
                table: "VariableXTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Curriculum",
                table: "VariableXTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GroupTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupValueTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupValueTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupValueTable_GroupTable_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariableTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariableTable_GroupTable_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariableValueTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableValueTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariableValueTable_GroupValueTable_GroupValueId",
                        column: x => x.GroupValueId,
                        principalTable: "GroupValueTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VariableValueTable_VariableTable_VariableId",
                        column: x => x.VariableId,
                        principalTable: "VariableTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTable_InstituteId",
                table: "GroupTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupValueTable_GroupId",
                table: "GroupValueTable",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VariableTable_GroupId",
                table: "VariableTable",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VariableValueTable_GroupValueId",
                table: "VariableValueTable",
                column: "GroupValueId");

            migrationBuilder.CreateIndex(
                name: "IX_VariableValueTable_VariableId",
                table: "VariableValueTable",
                column: "VariableId");
        }
    }
}
