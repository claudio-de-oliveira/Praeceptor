namespace Document.App.Models;

public class PlannerModel : Entity
{
    public string? Ementa { get; set; }
    public List<string> Objetivos { get; set; } = new();
    public string? Procedimentos { get; set; }
    public string? Avaliacao { get; set; }
    public List<ConceptKeyModel> Unidade1 { get; set; } = new();
    public List<ConceptKeyModel> Unidade2 { get; set; } = new();
    public List<BibItemValueModel> BibliografiaBasica { get; set; } = new();
    public List<BibItemValueModel> BibliografiaComplementar { get; set; } = new();
    public Guid ClassId { get; set; }
    public string DisciplinaId { get; set; } = default!;

    private static string ReferenceEncode(BibItemValueModel model)
    {
        string str = "";

        foreach (var autor in model.Autores.Nomes)
            str += "@Autor [ " + autor.Sobrenome + " ] [ " + autor.Nome + " ]\n";
        foreach (var tradutor in model.Tradutores.Nomes)
            str += "@Tradutor [ " + tradutor.Sobrenome + " ] [ " + tradutor.Nome + " ]\n";
        foreach (var organizador in model.Organizadores.Nomes)
            str += "@Organizador [ " + organizador.Sobrenome + " ] [ " + organizador.Nome + " ]\n";

        if (!string.IsNullOrEmpty(model.Title))
            str += "@Titulo [ " + model.Title.Trim() + " ]\n";
        if (!string.IsNullOrEmpty(model.Series))
            str += "@Serie [ " + model.Series.Trim() + " ]\n";

        if (!string.IsNullOrEmpty(model.Editor))
            str += "@Editor [ " + model.Editor.Trim() + " ]\n";

        if (model.Volume is not null)
            str += "@Volume [ " + model.Volume.Text1 + " ] [ " + model.Volume.Text2 + " ]\n";
        if (model.Edition > 0)
            str += "@Edicao [ " + model.Edition.ToString() + " ]\n";

        if (model.Exemplares > 0)
            str += "@Exemplares [ " + model.Exemplares.ToString() + " ]\n";
        if (model.Publisher is not null)
            str += "@Editora [ " + model.Publisher.Nome + " ] [ " + model.Publisher.Endereco + " ]\n";
        if (model.Year > 0)
            str += "@Ano [ " + model.Year.ToString() + " ]\n";

        if (!string.IsNullOrEmpty(model.Note))
            str += "@Nota [ " + model.Note.Trim() + " ]\n";
        if (!string.IsNullOrEmpty(model.ISBN))
            str += "@ISBN [ " + model.ISBN.Trim() + " ]\n";
        if (!string.IsNullOrEmpty(model.Details))
            str += "@Detalhes [ " + model.Details.Trim() + " ]\n";

        return str;
    }

    public static string Encode(PlannerModel planner)
    {
        string str = $"@PEA [ {planner.DisciplinaId} ]\n\n";

        if (!string.IsNullOrWhiteSpace(planner.Ementa))
            str += $"@Ementa [{planner.Ementa}]\n\n";

        // string competencias = "";
        //
        // if (!string.IsNullOrWhiteSpace(competencias))
        //     str += $"@Competencias\n{competencias}\n\n";

        string objetivos = "";

        foreach (string s in planner.Objetivos)
            objetivos += $"[{s}]\n";

        if (!string.IsNullOrWhiteSpace(objetivos))
            str += $"@Objetivos\n{objetivos}\n\n";

        if (planner.Unidade1 != null)
        {
            if (planner.Unidade1.Count > 0)
            {
                str += $"@Unidade1\n\n";

                foreach (var conceito in planner.Unidade1)
                {
                    str += $"@Chave [{conceito.Description}]\n";
                    foreach (var s in conceito.Conteudos)
                        str += $"[{s}]\n";
                    str += "\n";
                }
            }
        }
        if (planner.Unidade2 != null)
        {
            if (planner.Unidade2.Count > 0)
            {
                str += $"@Unidade2\n";

                foreach (var conceito in planner.Unidade2)
                {
                    str += $"@Chave [{conceito.Description}]\n";
                    foreach (var s in conceito.Conteudos)
                        str += $"[{s}]\n";
                    str += "\n";
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(planner.Procedimentos))
            str += $"@Procedimentos [\n{planner.Procedimentos}\n]\n\n";

        if (!string.IsNullOrWhiteSpace(planner.Avaliacao))
            str += $"@Avaliacao [\n{planner.Avaliacao}\n]\n\n";

        foreach (var bib in planner.BibliografiaBasica)
        {
            string star = bib.Online ? "*" : string.Empty;
            str += $"@Basica{star}\n{ReferenceEncode(bib)}\n";
        }
        str += "\n";
        foreach (var bib in planner.BibliografiaComplementar)
        {
            string star = bib.Online ? "*" : string.Empty;
            str += $"@Complementar{star}\n{ReferenceEncode(bib)}\n";
        }

        return str;
    }
}