using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraeceptorCQRS.Infrastructure.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileStreamTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStreamTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HoldingTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldingTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListNodeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreviousNodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NextNodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecondEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListNodeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListNodeTable_ListNodeTable_NextNodeId",
                        column: x => x.NextNodeId,
                        principalTable: "ListNodeTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ListNodeTable_ListNodeTable_PreviousNodeId",
                        column: x => x.PreviousNodeId,
                        principalTable: "ListNodeTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessageTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessageTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstituteTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    HoldingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituteTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstituteTable_HoldingTable_HoldingId",
                        column: x => x.HoldingId,
                        principalTable: "HoldingTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AxisTypeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AxisTypeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AxisTypeTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChapterTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassTypeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTypeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassTypeTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CEO = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AC = table.Column<int>(type: "int", nullable: false),
                    NumberOfSeasons = table.Column<int>(type: "int", nullable: false),
                    MinimumWorkload = table.Column<int>(type: "int", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "PreceptorDegreeTypeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreceptorDegreeTypeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreceptorDegreeTypeTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreceptorRegimeTypeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreceptorRegimeTypeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreceptorRegimeTypeTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubSectionTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSectionTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSectionTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubSubSectionTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSubSectionTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSubSectionTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Practice = table.Column<int>(type: "int", nullable: false),
                    Theory = table.Column<int>(type: "int", nullable: false),
                    PR = table.Column<int>(type: "int", nullable: false),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassTable_ClassTypeTable_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ClassTypeTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupValueTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "PreceptorTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DegreeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegimeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreceptorTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreceptorTable_InstituteTable_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "InstituteTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreceptorTable_PreceptorDegreeTypeTable_DegreeTypeId",
                        column: x => x.DegreeTypeId,
                        principalTable: "PreceptorDegreeTypeTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreceptorTable_PreceptorRegimeTypeTable_RegimeTypeId",
                        column: x => x.RegimeTypeId,
                        principalTable: "PreceptorRegimeTypeTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentTable",
                columns: table => new
                {
                    Season = table.Column<int>(type: "int", nullable: false),
                    Curriculum = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Optative = table.Column<bool>(type: "bit", nullable: false),
                    AxisTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTable", x => new { x.CourseId, x.Curriculum, x.Season, x.ClassId });
                    table.ForeignKey(
                        name: "FK_ComponentTable_AxisTypeTable_AxisTypeId",
                        column: x => x.AxisTypeId,
                        principalTable: "AxisTypeTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentTable_ClassTable_ClassId",
                        column: x => x.ClassId,
                        principalTable: "ClassTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponentTable_CourseTable_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CourseTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariableValueTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableValueTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariableValueTable_GroupValueTable_GroupValueId",
                        column: x => x.GroupValueId,
                        principalTable: "GroupValueTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariableValueTable_VariableTable_VariableId",
                        column: x => x.VariableId,
                        principalTable: "VariableTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AxisTypeTable_InstituteId",
                table: "AxisTypeTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterTable_InstituteId",
                table: "ChapterTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTable_InstituteId",
                table: "ClassTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTable_TypeId",
                table: "ClassTable",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTypeTable_InstituteId",
                table: "ClassTypeTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentTable_AxisTypeId",
                table: "ComponentTable",
                column: "AxisTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentTable_ClassId",
                table: "ComponentTable",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTable_InstituteId",
                table: "CourseTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTable_InstituteId",
                table: "DocumentTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTable_InstituteId",
                table: "GroupTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupValueTable_GroupId",
                table: "GroupValueTable",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituteTable_HoldingId",
                table: "InstituteTable",
                column: "HoldingId");

            migrationBuilder.CreateIndex(
                name: "IX_ListNodeTable_NextNodeId",
                table: "ListNodeTable",
                column: "NextNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ListNodeTable_PreviousNodeId",
                table: "ListNodeTable",
                column: "PreviousNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_PreceptorDegreeTypeTable_InstituteId",
                table: "PreceptorDegreeTypeTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_PreceptorRegimeTypeTable_InstituteId",
                table: "PreceptorRegimeTypeTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_PreceptorTable_DegreeTypeId",
                table: "PreceptorTable",
                column: "DegreeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PreceptorTable_InstituteId",
                table: "PreceptorTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_PreceptorTable_RegimeTypeId",
                table: "PreceptorTable",
                column: "RegimeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionTable_InstituteId",
                table: "SectionTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_SubSectionTable_InstituteId",
                table: "SubSectionTable",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_SubSubSectionTable_InstituteId",
                table: "SubSubSectionTable",
                column: "InstituteId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapterTable");

            migrationBuilder.DropTable(
                name: "ComponentTable");

            migrationBuilder.DropTable(
                name: "DocumentTable");

            migrationBuilder.DropTable(
                name: "FileStreamTable");

            migrationBuilder.DropTable(
                name: "ListNodeTable");

            migrationBuilder.DropTable(
                name: "OutboxMessageTable");

            migrationBuilder.DropTable(
                name: "PreceptorTable");

            migrationBuilder.DropTable(
                name: "SectionTable");

            migrationBuilder.DropTable(
                name: "SubSectionTable");

            migrationBuilder.DropTable(
                name: "SubSubSectionTable");

            migrationBuilder.DropTable(
                name: "VariableValueTable");

            migrationBuilder.DropTable(
                name: "AxisTypeTable");

            migrationBuilder.DropTable(
                name: "ClassTable");

            migrationBuilder.DropTable(
                name: "CourseTable");

            migrationBuilder.DropTable(
                name: "PreceptorDegreeTypeTable");

            migrationBuilder.DropTable(
                name: "PreceptorRegimeTypeTable");

            migrationBuilder.DropTable(
                name: "GroupValueTable");

            migrationBuilder.DropTable(
                name: "VariableTable");

            migrationBuilder.DropTable(
                name: "ClassTypeTable");

            migrationBuilder.DropTable(
                name: "GroupTable");

            migrationBuilder.DropTable(
                name: "InstituteTable");

            migrationBuilder.DropTable(
                name: "HoldingTable");
        }
    }
}
