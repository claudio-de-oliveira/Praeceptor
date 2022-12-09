using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.Values.PeaValues;

public class EditoraItem : ValueObject
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Nome;
        yield return Endereco;
    }
}