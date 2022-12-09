using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.Values.PeaValues;

public class ConceitoChave : ValueObject
{
    public string Description { get; set; } = string.Empty;

    public List<string> Conteudos { get; set; } = new();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Description;
        foreach (var conteudo in Conteudos)
            yield return conteudo;
    }
}