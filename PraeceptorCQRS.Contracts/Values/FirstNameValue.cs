namespace PraeceptorCQRS.Contracts.Values;

public class FirstNameValue : ValueObject
{
    public string Text { get; }

    public FirstNameValue(string text)
    {
        this.Text = text.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
    }
}