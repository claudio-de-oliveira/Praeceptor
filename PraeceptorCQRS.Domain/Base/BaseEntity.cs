namespace PraeceptorCQRS.Domain.Base
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        public Guid Id { get; private init; }

        protected BaseEntity(Guid guid)
        {
            this.Id = guid;
        }

        // public List<IDomainEvent> DomainEvents { get; set; } = new();

        #region Object identity
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            if (obj is not BaseEntity entity)
                return false;

            return entity.Id == Id;
        }

        public bool Equals(BaseEntity? other)
        {
            if (other is null)
                return false;

            if (other.GetType() != GetType())
                return false;

            return other.Id == Id;
        }

        public static bool operator ==(BaseEntity? first, BaseEntity? second)
            => first is not null && second is not null && first.Equals(second);
        public static bool operator !=(BaseEntity? first, BaseEntity? second)
            => !(first == second);

        public override int GetHashCode()
            => this.Id.GetHashCode();
        #endregion
    }
}

