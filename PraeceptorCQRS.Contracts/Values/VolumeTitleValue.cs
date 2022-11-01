namespace PraeceptorCQRS.Contracts.Values
{
    public class VolumeTitleValue : ValueObject
    {
        public string Text { get; }

        public VolumeTitleValue(string text)
        {
            this.Text = text.Trim();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }
    }
}
