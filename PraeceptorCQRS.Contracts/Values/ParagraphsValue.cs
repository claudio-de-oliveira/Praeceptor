namespace PraeceptorCQRS.Contracts.Values;

public class ParagraphsValue : ValueObject
{
    public string Text { get; }

    public ParagraphsValue(string text)
    {
        this.Text = text;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
    }
}
