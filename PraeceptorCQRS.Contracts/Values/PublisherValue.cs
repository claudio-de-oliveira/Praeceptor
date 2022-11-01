using Ardalis.GuardClauses;

namespace PraeceptorCQRS.Contracts.Values
{
    public class PublisherValue : ValueObject
    {
        public PublisherNameValue? Name { get; set; }
        public PublisherAddressValue? Address { get; set; }

        public PublisherValue(PublisherNameValue? name, PublisherAddressValue? address)
        {
            this.Address = address;
            this.Name = name;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Address?.Text))
                return $"{Name?.Text}, {Address?.Text}";
            Guard.Against.Null(Name);
            return Name.Text;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            if (Name is not null)
                yield return Name;
            if (Address is not null)
                yield return Address;
        }
    }
}
