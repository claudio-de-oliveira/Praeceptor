using Org.BouncyCastle.Bcpg.OpenPgp;

using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities;

public class SimpleTable : BaseAuditableEntity
{
    public SimpleTable()
        : base(Guid.Empty)
    { /* Nothing more todo */ }

    public SimpleTable(Guid id)
        : base(id)
    { /* Nothing more todo */ }

    public static SimpleTable Create(
        string code,
        string title,
        string? header,
        string rows,
        string? footer,
        Guid instituteId,
        string? createdBy
        )
        => new(Guid.NewGuid())
        {
            Code = code,
            Title = title,
            Header = header,
            Rows = rows,
            Footer = footer,
            Created = DateTime.UtcNow,
            InstituteId = instituteId,
            CreatedBy = createdBy
        };

    [Required, MaxLength(250)]
    public string Code { get; set; } = default!;

    [Required, MaxLength(250)]
    public string Title { get; set; } = default!;

    public string? Header { get; set; } = default!;

    public string Rows { get; set; } = default!;
    public string? Footer { get; set; } = default!;

    public Guid InstituteId { get; set; }

    [Required, ForeignKey("InstituteId")]
    public virtual Institute Institute { get; set; } = default!;

    private static string ConvertToRow(string row)
    {
        string[] cells = row.Split('&');

        string text = "\\row{\n";

        foreach (var cell in cells)
        {
            string[] tmp = cell.Split('\n');

            string s = "";
            for (int i = 0; i < tmp.Length; i++)
                s += tmp[i].Trim() + "\n";

            if (string.IsNullOrWhiteSpace(s))
                text +=
                    " \\cell[SHADING=Percent10; TABLECELLVERTICALALIGNMENT=Center]{\n" +
                    "  \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\n" +
                    "   \\run[FONTSIZE=8]{ }}}\n";
            else
                text +=
                    $" \\cell[SHADING=Percent10; TABLECELLVERTICALALIGNMENT=Center]{{{s}}}\n";
        }
        text += "}\n";

        return text;
    }

    public string Encode()
    {
        string text = "\\default[\n" +
               "KEEPNEXT;\n" +
               "KEEPLINES;\n" +
               "SPACINGBETWEENLINES=160;\n" +
               "FIRSTLINE=0;\n" +
               "JUSTIFICATION=center\n" +
               "]\n" +
               "{paragraph}\n" +
               "\n" +
               "\\default[\n" +
               "TOPMARGIN=100;\n" +
               "BOTTOMMARGIN=100;\n" +
               "TableCellVerticalAlignment=Center\n" +
               "]\n" +
               "{tablecell}\n" +
               "\n" +
               "\\default[\n" +
               "TOPBORDER=Double;\n" +
               "BOTTOMBORDER=Double;\n" +
               "LEFTBORDER=Double;\n" +
               "RIGHTBORDER=Double;\n" +
               "INSIDEHORIZONTALBORDER=Single;\n" +
               "INSIDEVERTICALBORDER=Single;\n" +
               "TABLECELLLEFTMARGIN=100;\n" +
               "TABLECELLRIGHTMARGIN=100;\n" +
               "JUSTIFICATION=Center;\n" +
               "TABLEOVERLAP=Never\n" +
               "]\n" +
               "{table}\n" +
               "\n" +
               "\\table{\n";

        if (Header is not null)
            text += ConvertToRow(Header);

        string[] rows = Rows.Split("&&");
        foreach (var row in rows)
            text += ConvertToRow(row);

        if (Footer is not null)
            text += ConvertToRow(Footer);

        text +=
               "}\n" +
               "\n" +
               // $"    \\caption[BEFORE=300;JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=360]{{{Title}}}\n" +
               // "\n" +
               // "}\n" +
               "\n" +
               "\\default{paragraph}\n" +
               "\\default{tablecell}\n" +
               "\\default{table}\n" +
               "\\paragraph[AFTER=100]{\\run{ }}\n";

        return text;
    }
}