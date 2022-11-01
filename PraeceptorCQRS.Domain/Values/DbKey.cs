using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.Values
{
    public class DbKeyX<T> : ValueObject
    {
        public Guid Id { get; } = default!;

        private DbKeyX(Guid id)
        {
            this.Id = id;
        }

        public static DbKeyX<T> Create(Guid id)
            => new(id);
        public static DbKeyX<T>? Create(Guid? id)
            => id is not null ? new DbKeyX<T>((Guid)id) : null;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
