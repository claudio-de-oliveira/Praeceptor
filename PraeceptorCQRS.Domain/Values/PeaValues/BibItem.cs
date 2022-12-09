using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.Values.PeaValues;

public class BibItem : ValueObject
{
    // Campos obrigatórios: author/editor, title, publisher, year
    public ListOfNomes Autores { get; set; }

    public ListOfNomes Tradutores { get; set; }
    public ListOfNomes Organizadores { get; set; }
    public string? Editor { get; set; }
    public int Exemplares { get; set; }
    public string Title { get; set; }
    public EditoraItem Publisher { get; set; }
    public int Year { get; set; }

    // Campos opcionais: volume/number, series, address, edition, month, note, key
    public VolumeItem Volume { get; set; }

    public string? Series { get; set; }
    public int Edition { get; set; }
    public string? Note { get; set; }
    public string? ISBN { get; set; }

    public bool Online { get; set; }
    public string? Details { get; set; }

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
            foreach (NomeItem autor in Autores.Nomes)
                str += "     @Autor [ " + autor.Sobrenome + " ] [ " + autor.Nome + " ]\n";
        if (Tradutores is not null)
            foreach (NomeItem tradutor in Tradutores.Nomes)
                str += "     @Tradutor [ " + tradutor.Sobrenome + " ] [ " + tradutor.Nome + " ]\n";
        if (Organizadores is not null)
            foreach (NomeItem organizador in Organizadores.Nomes)
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

        if (Autores.Nomes.Count == 0 && Tradutores.Nomes.Count == 0 && Organizadores.Nomes.Count == 0)
        {
            str += $"______. ";
        }
        else
        {
            if (Autores.Nomes.Count > 0)
            {
                for (int i = 0; i < Autores.Nomes.Count - 1; i++)
                    str += $"{Autores.Nomes[i].Sobrenome}, {Autores.Nomes[i].Nome}; ";
                str += $"{Autores.Nomes[^1].Sobrenome}, {Autores.Nomes[^1].Nome}. ";
            }
            else if (Tradutores.Nomes.Count > 0)
            {
                for (int i = 0; i < Tradutores.Nomes.Count - 1; i++)
                    str += $"{Tradutores.Nomes[i].Sobrenome}, {Tradutores.Nomes[i].Nome}; ";
                str += $"{Tradutores.Nomes[^1].Sobrenome}, {Tradutores.Nomes[^1].Nome} (Tradutor(es)). ";
            }
            else if (Organizadores.Nomes.Count > 0)
            {
                for (int i = 0; i < Organizadores.Nomes.Count - 1; i++)
                    str += $"{Organizadores.Nomes[i].Sobrenome}, {Organizadores.Nomes[i].Nome}; ";
                str += $"{Organizadores.Nomes[^1].Sobrenome}, {Organizadores.Nomes[^1].Nome} (Organizador(es)). ";
            }
        }

        if (!string.IsNullOrEmpty(Title))
            str += $"{Title}. ";
        if (!string.IsNullOrEmpty(Series))
            str += $"Série: {Series}. ";

        if (!string.IsNullOrEmpty(Editor))
            str += $"{Editor}. ";

        if (Volume is not null)
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

        if (Publisher is not null)
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        yield return Autores;
        yield return Tradutores;
        yield return Organizadores;
        yield return Editor!;
        yield return Exemplares;
        yield return Publisher;
        yield return Year;
        yield return Volume;
        yield return Series!;
        yield return Edition;
        yield return Note!;
        yield return ISBN!;
        yield return Online;
        yield return Details!;
    }
}