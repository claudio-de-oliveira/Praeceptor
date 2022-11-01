namespace PraeceptorCQRS.Contracts.Values
{
    public class PublisherNameValue : ValueObject
    {
        public string Text { get; set; }

        public PublisherNameValue(string name)
        {
            this.Text = name.Trim();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }
    }
}
