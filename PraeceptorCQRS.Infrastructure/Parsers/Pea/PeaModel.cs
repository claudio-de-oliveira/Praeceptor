namespace PraeceptorCQRS.Infrastructure.Parsers.Pea;

public class NomeItem
{
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;

    public NomeItem()
    { /* Nothing more todo */ }

    protected NomeItem(string nome, string sobrenome)
    {
        this.Nome = nome.Trim();
        this.Sobrenome = sobrenome.Trim();
    }

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(Nome) && !string.IsNullOrEmpty(Sobrenome))
            return $"{Sobrenome.ToUpper()}, {Nome}";
        if (!string.IsNullOrEmpty(Nome))
            return Nome;
        return Sobrenome;
    }
}

public class VolumeItem
{
    public string Text1 { get; set; }
    public string Text2 { get; set; }

    public VolumeItem()
    {
        this.Text1 = string.Empty;
        this.Text2 = string.Empty;
    }

    public VolumeItem(string text1, string text2)
    {
        this.Text1 = text1.Trim();
        this.Text2 = text2.Trim();
    }
}

public class EditoraItem
{
    public string Nome { get; set; }
    public string Endereco { get; set; }

    public EditoraItem()
    {
        this.Endereco = string.Empty;
        this.Nome = string.Empty;
    }

    public EditoraItem(string endereco, string nome)
    {
        Console.WriteLine(ToString());
        this.Endereco = endereco.Trim();
        this.Nome = nome.Trim();
    }

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(Endereco))
            return $"{Nome}, {Endereco}";
        return Nome;
    }
}

public class ListOfNomes : List<NomeItem>
{
    public ListOfNomes()
    { /* Nothing more todo */ }

    public ListOfNomes(string text)
    {
        string txt = text + "#";
        string nome = string.Empty, sobrenome = string.Empty;
        int pos = 0;
        int state = 0;

        while (true)
        {
            if (txt[pos] == '#')
            {
                if (state == 10)
                    this.Add(new NomeItem { Nome = nome.Trim(), Sobrenome = sobrenome.Trim() });
                return;
            }

            switch (state)
            {
                case 0:
                    nome = string.Empty;
                    sobrenome = string.Empty;

                    if (char.IsWhiteSpace(txt[pos]))
                    {
                        pos++;
                        state = 0;
                        break;
                    }
                    if (char.IsLetter(txt[pos]))
                    {
                        sobrenome += txt[pos];
                        pos++;
                        state = 1;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 1: // sobrenome
                    if (char.IsLetter(txt[pos]))
                    {
                        sobrenome += txt[pos];
                        pos++;
                        state = 1;
                        break;
                    }
                    if (char.IsWhiteSpace(txt[pos]))
                    {
                        sobrenome += txt[pos];
                        pos++;
                        state = 2;
                        break;
                    }
                    if (txt[pos] == '.')
                    {
                        sobrenome += txt[pos];
                        pos++;
                        state = 3;
                        break;
                    }
                    if (txt[pos] == ',')
                    {
                        pos++;
                        state = 5;
                        break;
                    }
                    if (txt[pos] == ';')
                    {
                        pos++;
                        state = 10;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 2:
                    if (char.IsWhiteSpace(txt[pos]))
                    {
                        pos++;
                        state = 2;
                        break;
                    }
                    if (char.IsLetter(txt[pos]))
                    {
                        sobrenome += txt[pos];
                        pos++;
                        state = 1;
                        break;
                    }
                    if (txt[pos] == ',')
                    {
                        pos++;
                        state = 5;
                        break;
                    }
                    if (txt[pos] == ';')
                    {
                        pos++;
                        state = 10;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 3:
                    if (txt[pos] == ',')
                    {
                        pos++;
                        state = 5;
                        break;
                    }
                    if (char.IsWhiteSpace(txt[pos]))
                    {
                        sobrenome += txt[pos];
                        pos++;
                        state = 4;
                        break;
                    }
                    if (char.IsLetter(txt[pos]))
                    {
                        // insere um espaço entre o ponto e a próxima letra
                        sobrenome += ' ';
                        //
                        sobrenome += txt[pos];
                        pos++;
                        state = 1;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 4:
                    if (txt[pos] == ',')
                    {
                        pos++;
                        state = 5;
                        break;
                    }
                    if (txt[pos] == ';')
                    {
                        pos++;
                        state = 10;
                        break;
                    }
                    if (char.IsLetter(txt[pos]))
                    {
                        sobrenome += txt[pos];
                        pos++;
                        state = 1;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 5:
                    if (char.IsWhiteSpace(txt[pos]))
                    {
                        pos++;
                        state = 5;
                        break;
                    }
                    if (char.IsLetter(txt[pos]))
                    {
                        nome += txt[pos];
                        pos++;
                        state = 6;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 6: // nome
                    if (char.IsLetter(txt[pos]))
                    {
                        nome += txt[pos];
                        pos++;
                        state = 6;
                        break;
                    }
                    if (char.IsWhiteSpace(txt[pos]))
                    {
                        nome += txt[pos];
                        pos++;
                        state = 7;
                        break;
                    }
                    if (txt[pos] == '.')
                    {
                        nome += txt[pos];
                        pos++;
                        state = 8;
                        break;
                    }
                    if (txt[pos] == ';')
                    {
                        pos++;
                        state = 10;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 7:
                    if (char.IsWhiteSpace(txt[pos]))
                    {
                        pos++;
                        state = 7;
                        break;
                    }
                    if (char.IsLetter(txt[pos]))
                    {
                        nome += txt[pos];
                        pos++;
                        state = 6;
                        break;
                    }
                    if (txt[pos] == ';')
                    {
                        pos++;
                        state = 10;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 8:
                    if (txt[pos] == ';')
                    {
                        pos++;
                        state = 10;
                        break;
                    }
                    if (char.IsWhiteSpace(txt[pos]))
                    {
                        nome += txt[pos];
                        pos++;
                        state = 9;
                        break;
                    }
                    if (char.IsLetter(txt[pos]))
                    {
                        // insere um espaço entre o ponto e a próxima letra
                        nome += ' ';
                        //
                        nome += txt[pos];
                        pos++;
                        state = 6;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 9:
                    if (txt[pos] == ';')
                    {
                        pos++;
                        state = 10;
                        break;
                    }
                    if (char.IsLetter(txt[pos]))
                    {
                        nome += txt[pos];
                        pos++;
                        state = 6;
                        break;
                    }
                    // OUTRO
                    pos++;
                    state = 0;
                    break;

                case 10:
                    this.Add(new NomeItem { Nome = nome.Trim(), Sobrenome = sobrenome.Trim() });
                    pos++;
                    state = 0;
                    break;
            }
        }
    }

    public override string ToString()
    {
        string str = string.Empty;
        foreach (NomeItem nome in this)
            str += $"{nome}; ";
        return str;
    }
}

public class BibItem
{
    public Guid Key { get; set; }

    // Campos obrigatórios: author/editor, title, publisher, year
    public ListOfNomes Autores { get; set; }
    public ListOfNomes Tradutores { get; set; }
    public ListOfNomes Organizadores { get; set; }
    public string Editor { get; set; }
    public int Exemplares { get; set; }
    public string Title { get; set; }
    public EditoraItem Publisher { get; set; }
    public int Year { get; set; }

    // Campos opcionais: volume/number, series, address, edition, month, note, key
    public VolumeItem Volume { get; set; }
    public string Series { get; set; }
    public int Edition { get; set; }
    public string Note { get; set; }
    public string ISBN { get; set; }

    public bool Online { get; set; }
    public string Details { get; set; }

    public bool ShowDetails { get; set; }

    public BibItem()
    {
        Autores = new ListOfNomes();
        Tradutores = new ListOfNomes();
        Organizadores = new ListOfNomes();

        Title = "";
        Editor = "";
        Publisher = new EditoraItem();
        Year = 0;
        Exemplares = 0;

        Volume = new VolumeItem();
        Series = "";
        Edition = 0;
        Note = "";
        ISBN = "";

        Online = false;

        Details = "";
    }

    public BibItem(string title)
        : this()
    {
        Title = title.Trim(new char[] { ' ', '\n', '\t', '\f' });
    }

    public string Encode()
    {
        string str = "";

        if (Autores is not null)
            foreach (NomeItem autor in Autores)
                str += "     @Autor [ " + autor.Sobrenome + " ] [ " + autor.Nome + " ]\n";
        if (Tradutores is not null)
            foreach (NomeItem tradutor in Tradutores)
                str += "     @Tradutor [ " + tradutor.Sobrenome + " ] [ " + tradutor.Nome + " ]\n";
        if (Organizadores is not null)
            foreach (NomeItem organizador in Organizadores)
                str += "     @Organizador [ " + organizador.Sobrenome + " ] [ " + organizador.Nome + " ]\n";

        if (!string.IsNullOrEmpty(Title))
            str += "     @Titulo [ " + Title.Trim() + " ]\n";
        if (!string.IsNullOrEmpty(Series))
            str += "     @Serie [ " + Series.Trim() + " ]\n";

        if (!string.IsNullOrEmpty(Editor))
            str += "     @Editor [ " + Editor.Trim() + " ]\n";

        if (Volume is not null)
            str += "     @Volume [ " + Volume.Text1 + " ] [ " + Volume.Text2 + " ]\n";
        if (Edition > 0)
            str += "     @Edicao [ " + Edition.ToString() + " ]\n";

        if (Exemplares > 0)
            str += "     @Exemplares [ " + Exemplares.ToString() + " ]\n";
        if (Publisher is not null)
            str += "     @Editora [ " + Publisher.Nome + " ] [ " + Publisher.Endereco + " ]\n";
        if (Year > 0)
            str += "     @Ano [ " + Year.ToString() + " ]\n";

        if (!string.IsNullOrEmpty(Note))
            str += "     @Nota [ " + Note.Trim() + " ]\n";
        if (!string.IsNullOrEmpty(ISBN))
            str += "     @ISBN [ " + ISBN.Trim() + " ]\n";
        if (!string.IsNullOrEmpty(Details))
            str += "     @Detalhes [ " + Details.Trim() + " ]\n";

        return str;
    }

    public override string ToString()
    {
        string str = "";

        if (Autores.Count == 0 && Tradutores.Count == 0 && Organizadores.Count == 0)
        {
            str += $"______. ";
        }
        else
        {
            if (Autores.Count > 0)
            {
                for (int i = 0; i < Autores.Count - 1; i++)
                    str += $"{Autores[i].Sobrenome}, {Autores[i].Nome}; ";
                str += $"{Autores[^1].Sobrenome}, {Autores[^1].Nome}. ";
            }
            else if (Tradutores.Count > 0)
            {
                for (int i = 0; i < Tradutores.Count - 1; i++)
                    str += $"{Tradutores[i].Sobrenome}, {Tradutores[i].Nome}; ";
                str += $"{Tradutores[^1].Sobrenome}, {Tradutores[^1].Nome} (Tradutor(es)). ";
            }
            else if (Organizadores.Count > 0)
            {
                for (int i = 0; i < Organizadores.Count - 1; i++)
                    str += $"{Organizadores[i].Sobrenome}, {Organizadores[i].Nome}; ";
                str += $"{Organizadores[^1].Sobrenome}, {Organizadores[^1].Nome} (Organizador(es)). ";
            }
        }

        if (!string.IsNullOrEmpty(Title))
            str += $"{Title}. ";
        if (!string.IsNullOrEmpty(Series))
            str += $"Série: {Series}. ";

        if (!string.IsNullOrEmpty(Editor))
            str += $"{Editor}. ";

        if (Volume != null)
        {
            if (!string.IsNullOrWhiteSpace(Volume.Text1))
            {
                if (!string.IsNullOrWhiteSpace(Volume.Text2))
                    str += $"Volume {Volume.Text1} – {Volume.Text2}. ";
                else
                    str += $"Volume {Volume.Text1}. ";
            }
        }
        if (Edition > 0)
            str += $"{Edition}a edição. ";

        if (Publisher != null)
        {
            if (!string.IsNullOrWhiteSpace(Publisher.Nome))
            {
                if (!string.IsNullOrWhiteSpace(Publisher.Endereco))
                    str += $"{Publisher.Nome}, {Publisher.Endereco}. ";
                else
                    str += $"{Publisher.Nome}. ";
            }
        }
        if (Year > 0)
            str += $"{Year}. ";

        return str;
    }
}

public class ConceitoChave
{
    public Guid Key { get; set; }
    public bool ShowDetails { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Conteudos { get; set; } = new();
}

public class PeaModel
{
    public int Id { get; set; }
    public string Ementa { get; set; }
    public List<string> Objetivos { get; set; }
    public string Procedimentos { get; set; }
    public string Avaliacao { get; set; }
    public List<ConceitoChave> Unidade1 { get; set; }
    public List<ConceitoChave> Unidade2 { get; set; }

    public List<BibItem> BibliografiaBasica { get; set; }
    public List<BibItem> BibliografiaComplementar { get; set; }

    public int ClassId { get; set; }
    public string DisciplinaId { get; set; }

    public PeaModel()
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
