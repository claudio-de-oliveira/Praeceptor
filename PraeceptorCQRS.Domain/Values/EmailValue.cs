using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.Values
{
    public class EmailValueX : ValueObject
    {
        public string Value { get; } = default!;

        private EmailValueX(string email)
        {
            this.Value = email;
        }

        public static EmailValueX? Create(string? email)
            => email is not null ? new EmailValueX(email) : null;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToUpper();
        }
    }
}
