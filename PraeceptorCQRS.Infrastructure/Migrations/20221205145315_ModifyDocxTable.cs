using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;

using Microsoft.EntityFrameworkCore.Migrations;

using System.IO;

#nullable disable

namespace PraeceptorCQRS.Infrastructure.Migrations
{
    public partial class ModifyDocxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "DocxStreamTable");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "DocxStreamTable");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DocxStreamTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DocxStreamTable");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DocxStreamTable",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "DocxStreamTable",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);
        }
    }
}

/*
USE [PrCQRS_Docx_DB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DocxStreamTable] (
    [Id][uniqueidentifier] ROWGUIDCOL NOT NULL,
    [Title] [nvarchar] (4000) NULL,
	[Description][nvarchar] (4000) NULL,
	[InstituteId][uniqueidentifier] NOT NULL,
    [ContentType] [nvarchar] (500) NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [CreatedBy] [nvarchar] (250) NULL,
    [Data] [varbinary] (max)FILESTREAM  NOT NULL,
PRIMARY KEY CLUSTERED
(

    [Id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY] FILESTREAM_ON[DocxStreamData],
UNIQUE NONCLUSTERED
(
    [Id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
) ON[PRIMARY] FILESTREAM_ON[DocxStreamData]
GO

ALTER TABLE [dbo].[DocxStreamTable] ADD DEFAULT(newid()) FOR[Id]
GO

 */