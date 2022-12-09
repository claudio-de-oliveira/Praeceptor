using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.Values.PeaValues;

public class NomeItem : ValueObject
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Nome;
        yield return Sobrenome;
    }
}