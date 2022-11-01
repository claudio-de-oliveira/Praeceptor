using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.Values
{
    public class ParagraphsValueX : ValueObject
    {
        public string Text { get; }

        public ParagraphsValueX(string text)
        {
            this.Text = text;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }
    }
}
