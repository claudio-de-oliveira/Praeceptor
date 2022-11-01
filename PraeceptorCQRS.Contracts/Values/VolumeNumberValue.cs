namespace PraeceptorCQRS.Contracts.Values
{
    public class VolumeNumberValue : ValueObject
    {
        public string Text { get; }

        public VolumeNumberValue(string text)
        {
            this.Text = text.Trim();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }
    }
}
