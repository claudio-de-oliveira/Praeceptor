using PraeceptorCQRS.Contracts.Values;

using System;

namespace PraeceptorCQRS.Contracts.Entities.Pea
{
    public class PeaResponse : ValueObject
    {
        public Guid Id { get; }
        public string Ementa { get; } = default!;
        public List<string> Objetivos { get; } = new();
        public string Procedimentos { get; } = default!;
        public string Avaliacao { get; } = default!;
        public List<KeyConcept> Unidade1 { get; } = new();
        public List<KeyConcept> Unidade2 { get; } = new();

        public List<ReferenceValue> BibliografiaBasica { get; } = new();
        public List<ReferenceValue> BibliografiaComplementar { get; } = new();

        public Guid ClassId { get; }
        public string DisciplinaId { get; } = default!;

        // public PeaResponse()
        // {
        //     this.DisciplinaId = "";
        //     this.Avaliacao = "";
        //     this.Procedimentos = "";
        //     this.Ementa = "";
        // }

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

                    foreach (var conceito in Unidade1)
                    {
                        str += $"     @Chave [{conceito.Description}]\n";
                        foreach (var s in conceito.Contents)
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

                    foreach (var conceito in Unidade2)
                    {
                        str += $"     @Chave [{conceito.Description}]\n";
                        foreach (var s in conceito.Contents)
                            str += $"          [{s}]\n";
                        str += "\n";
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(Procedimentos))
                str += $"@Procedimentos [\n{Procedimentos}\n]\n\n";

            if (!string.IsNullOrWhiteSpace(Avaliacao))
                str += $"@Avaliacao [\n{Avaliacao}\n]\n\n";

            foreach (var bib in BibliografiaBasica)
            {
                string star = bib.Online ? "*" : string.Empty;

                str += $"@Basica{star}\n{bib.Encode()}\n";
            }
            str += "\n";
            foreach (var bib in BibliografiaComplementar)
            {
                string star = bib.Online ? "*" : string.Empty;

                str += $"@Complementar{star}\n{bib.Encode()}\n";
            }

            return str;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Ementa;
            yield return ClassId;
            foreach (var objective in Objetivos)
                yield return objective;
            yield return Procedimentos;
            yield return Avaliacao;

            foreach (var concept in Unidade1)
                yield return concept;
            foreach (var concept in Unidade2)
                yield return concept;

            foreach (var reference in BibliografiaBasica)
                yield return reference;
            foreach (var reference in BibliografiaComplementar)
                yield return reference;

            yield return DisciplinaId;
        }
    }
}
