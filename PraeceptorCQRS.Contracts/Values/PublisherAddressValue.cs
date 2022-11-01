namespace PraeceptorCQRS.Contracts.Values;

public class PublisherAddressValue : ValueObject
{
    public string Text { get; set; }

    public PublisherAddressValue(string address)
    {
        this.Text = address.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
    }
}
