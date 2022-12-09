using PraeceptorCQRS.Domain.Base;
using PraeceptorCQRS.Domain.Values.PeaValues;

namespace PraeceptorCQRS.Domain.Entities;

public class PeaModel : BaseAuditableEntity
{
    public string Ementa { get; set; }
    public List<string> Objetivos { get; set; }
    public string Procedimentos { get; set; }
    public string Avaliacao { get; set; }
    public List<ConceitoChave> Unidade1 { get; set; }
    public List<ConceitoChave> Unidade2 { get; set; }

    public List<BibItem> BibliografiaBasica { get; set; }
    public List<BibItem> BibliografiaComplementar { get; set; }

    public Guid ClassId { get; set; }
    public string DisciplinaId { get; set; }

    public PeaModel(Guid id)
        : base(id)
    {
        this.DisciplinaId = "";
        this.Unidade1 = new List<ConceitoChave>();
        this.Unidade2 = new List<ConceitoChave>();
        this.Avaliacao = "";
        this.Procedimentos = "";
        this.Objetivos = new List<string>();
        this.Ementa = "";
        this.BibliografiaBasica = new List<BibItem>();
        this.BibliografiaComplementar = new List<BibItem>();
    }

    public PeaModel(Guid id,
        string ementa,
        List<string> objetivos,
        string procedimentos,
        string avaliacao,
        List<ConceitoChave> unidade1,
        List<ConceitoChave> unidade2,
        List<BibItem> bibliografiaBasica,
        List<BibItem> bibliografiaComplementar,
        Guid classId,
        string disciplinaId,
        DateTime created,
        string? createdBy,
        DateTime? lastModified,
        string? lastModifiedBy
        )
        : base(id)
    {
        this.Ementa = ementa;
        this.Objetivos = objetivos;
        this.Procedimentos = procedimentos;
        this.Avaliacao = avaliacao;
        this.Unidade1 = unidade1;
        this.Unidade2 = unidade2;
        this.BibliografiaBasica = bibliografiaBasica;
        this.BibliografiaComplementar = bibliografiaComplementar;
        this.ClassId = classId;
        this.DisciplinaId = disciplinaId;
        this.Created = created;
        this.CreatedBy = createdBy;
        this.LastModified = lastModified;
        this.LastModifiedBy = lastModifiedBy;
    }

    public string Encode()
    {
        string str = $"@PEA [ {DisciplinaId} ]\n\n";

        if (!string.IsNullOrWhiteSpace(Ementa))
            str += $"@Ementa [{Ementa}]\n\n";

        string competencias = "";

        if (!string.IsNullOrWhiteSpace(competencias))
            str += $"@Competencias\n{competencias}\n\n";

        string objetivos = "";

        foreach (string s in Objetivos)
            objetivos += $"          [{s}]\n";

        if (!string.IsNullOrWhiteSpace(objetivos))
            str += $"@Objetivos\n{objetivos}\n\n";

        if (Unidade1 != null)
        {
            if (Unidade1.Count > 0)
            {
                str += $"@Unidade1\n\n";

                foreach (ConceitoChave conceito in Unidade1)
                {
                    str += $"     @Chave [{conceito.Description}]\n";
                    foreach (var s in conceito.Conteudos)
                        str += $"          [{s}]\n";
                    str += "\n";
                }
            }
        }
        if (Unidade2 != null)
        {
            if (Unidade2.Count > 0)
            {
                str += $"@Unidade2\n";

                foreach (ConceitoChave conceito in Unidade2)
                {
                    str += $"     @Chave [{conceito.Description}]\n";
                    foreach (var s in conceito.Conteudos)
                        str += $"          [{s}]\n";
                    str += "\n";
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(Procedimentos))
            str += $"@Procedimentos [\n{Procedimentos}\n]\n\n";

        if (!string.IsNullOrWhiteSpace(Avaliacao))
            str += $"@Avaliacao [\n{Avaliacao}\n]\n\n";

        foreach (BibItem bib in BibliografiaBasica)
        {
            string star = bib.Online ? "*" : string.Empty;

            str += $"@Basica{star}\n{bib.Encode()}\n";
        }
        str += "\n";
        foreach (BibItem bib in BibliografiaComplementar)
        {
            string star = bib.Online ? "*" : string.Empty;

            str += $"@Complementar{star}\n{bib.Encode()}\n";
        }

        return str;
    }
}