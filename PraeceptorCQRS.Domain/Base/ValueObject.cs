// Objects and data items in a system that do not require an identity and identity tracking.

namespace PraeceptorCQRS.Domain.Base
{
    // Learn more: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects
    // Value objects should be immutable. If I want to change my party date, I create a new object instead.
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        #region Equality
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
                return false;
            return ReferenceEquals(left, right) || left!.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
            => !(EqualOperator(left, right));

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject one, ValueObject two)
            => EqualOperator(one, two);
        public static bool operator !=(ValueObject one, ValueObject two)
            => NotEqualOperator(one, two);
        #endregion
    }
}

