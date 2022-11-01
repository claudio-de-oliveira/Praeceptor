namespace PraeceptorCQRS.Contracts.Values;

public class SecondNameValue : ValueObject
{
    public string Text { get; }

    public SecondNameValue(string text)
    {
        this.Text = text.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
    }
}
