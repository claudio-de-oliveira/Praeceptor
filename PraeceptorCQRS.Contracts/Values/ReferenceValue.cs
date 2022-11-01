namespace PraeceptorCQRS.Contracts.Values
{
    public class ReferenceValue : ValueObject
    {
        public Guid Key { get; }

        // Campos obrigatórios: author/editor, title, publisher, year
        public ListOfNamesValue? Authors { get; }
        public ListOfNamesValue? Traductors { get; }
        public ListOfNamesValue? Organizators { get; }
        public string? Editor { get; }
        public int NumberOfExemplars { get; }
        public string? Title { get; }
        public PublisherValue? Publisher { get; }
        public int Year { get; }

        // Campos opcionais: volume/number, series, address, edition, month, note, key
        public VolumeValue? Volume { get; }
        public string? Series { get; }
        public int Edition { get; }
        public string? Note { get; }
        public string? ISBN { get; }

        public bool Online { get; }
        public string? Details { get; }

        public ReferenceValue(string title)
        {
            Title = title.Trim(new char[] { ' ', '\n', '\t', '\f' });
        }

        public string Encode()
        {
            string str = "";

            if (Authors is not null)
                foreach (var name in Authors.Names)
                    str += "     @Autor [ " + name.SecondName?.Text + " ] [ " + name.FirstName?.Text + " ]\n";
            if (Traductors is not null)
                foreach (var name in Traductors.Names)
                    str += "     @Tradutor [ " + name.SecondName?.Text + " ] [ " + name.FirstName?.Text + " ]\n";
            if (Organizators is not null)
                foreach (var name in Organizators.Names)
                    str += "     @Organizador [ " + name.SecondName?.Text + " ] [ " + name.FirstName?.Text + " ]\n";

            if (!string.IsNullOrEmpty(Title))
                str += "     @Titulo [ " + Title + " ]\n";
            if (!string.IsNullOrEmpty(Series))
                str += "     @Serie [ " + Series + " ]\n";

            if (!string.IsNullOrEmpty(Editor))
                str += "     @Editor [ " + Editor + " ]\n";

            if (Volume is not null)
                str += "     @Volume [ " + Volume.VolumeNumber?.Text + " ] [ " + Volume.VolumeTitle?.Text + " ]\n";
            if (Edition > 0)
                str += "     @Edicao [ " + Edition + " ]\n";

            if (NumberOfExemplars > 0)
                str += "     @Exemplares [ " + NumberOfExemplars + " ]\n";
            if (Publisher is not null)
                str += "     @Editora [ " + Publisher.Name + " ] [ " + Publisher.Address + " ]\n";
            if (Year > 0)
                str += "     @Ano [ " + Year + " ]\n";

            if (!string.IsNullOrEmpty(Note))
                str += "     @Nota [ " + Note + " ]\n";
            if (!string.IsNullOrEmpty(ISBN))
                str += "     @ISBN [ " + ISBN.Trim() + " ]\n";
            if (!string.IsNullOrEmpty(Details))
                str += "     @Detalhes [ " + Details + " ]\n";

            return str;
        }

        public override string ToString()
        {
            string str = "";

            if (Authors?.Names.Count == 0 && Traductors?.Names.Count == 0 && Organizators?.Names.Count == 0)
            {
                str += $"______. ";
            }
            else
            {
                if (Authors?.Names.Count > 0)
                {
                    for (int i = 0; i < Authors?.Names.Count - 1; i++)
                        str += $"{Authors?.Names[i].SecondName}, {Authors?.Names[i].FirstName}; ";
                    str += $"{Authors?.Names[^1].SecondName}, {Authors?.Names[^1].FirstName}. ";
                }
                else if (Traductors?.Names.Count > 0)
                {
                    for (int i = 0; i < Traductors?.Names.Count - 1; i++)
                        str += $"{Traductors?.Names[i].SecondName}, {Traductors?.Names[i].FirstName}; ";
                    str += $"{Traductors?.Names[^1].SecondName}, {Traductors?.Names[^1].FirstName} (Tradutor(es)). ";
                }
                else if (Organizators?.Names.Count > 0)
                {
                    for (int i = 0; i < Organizators?.Names.Count - 1; i++)
                        str += $"{Organizators?.Names[i].SecondName}, {Organizators?.Names[i].FirstName}; ";
                    str += $"{Organizators?.Names[^1].SecondName}, {Traductors?.Names[^1].FirstName} (Organizador(es)). ";
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
                if (!string.IsNullOrWhiteSpace(Volume.VolumeNumber?.Text))
                {
                    if (!string.IsNullOrWhiteSpace(Volume.VolumeTitle?.Text))
                        str += $"Volume {Volume.VolumeNumber.Text} – {Volume.VolumeTitle.Text}. ";
                    else
                        str += $"Volume {Volume.VolumeNumber}. ";
                }
            }
            if (Edition > 0)
                str += $"{Edition}a edição. ";

            if (Publisher is not null)
            {
                if (!string.IsNullOrWhiteSpace(Publisher.Name?.Text))
                {
                    if (!string.IsNullOrWhiteSpace(Publisher.Address?.Text))
                        str += $"{Publisher.Name.Text}, {Publisher.Address?.Text}. ";
                    else
                        str += $"{Publisher.Name.Text}. ";
                }
            }
            if (Year > 0)
                str += $"{Year}. ";

            return str;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            if (Authors is not null)
                foreach (var name in Authors.Names)
                    yield return name;

            if (!string.IsNullOrWhiteSpace(Title))
                yield return Title;

            if (!string.IsNullOrWhiteSpace(Editor))
                yield return Editor;

            yield return Year;

            yield return Edition;

            yield return Online;

            if (Publisher is not null)
                yield return Publisher;

            if (Volume is not null)
                yield return Volume;

            yield return NumberOfExemplars;

            if (!string.IsNullOrWhiteSpace(Series))
                yield return Series;

            if (!string.IsNullOrWhiteSpace(Note))
                yield return Note;

            if (!string.IsNullOrWhiteSpace(ISBN))
                yield return ISBN;

            if (!string.IsNullOrWhiteSpace(Details))
                yield return Details;

            if (Traductors is not null)
                foreach (var name in Traductors.Names)
                    yield return name;
            if (Organizators is not null)
                foreach (var name in Organizators.Names)
                    yield return name;
        }
    }
}
