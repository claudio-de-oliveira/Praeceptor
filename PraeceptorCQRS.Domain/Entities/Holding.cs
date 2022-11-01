using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Holding : AggregateRoot
    {
        public Holding(Guid id)
            : base(id)
        {
            Institutes = new HashSet<Institute>();
        }

        public static Holding Create(
            string acronym,
            string name,
            string? address,
            DateTime created,
            string? createdBy
            )
            =>
            new(Guid.Empty)
            {
                Acronym = acronym,
                Name = name,
                Address = address,
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        [Required, MaxLength(20)]
        public string Acronym { get; set; } = null!;
        [Required, MaxLength(250)]
        public string Name { get; set; } = null!;
        [MaxLength(4000)]
        public string? Address { get; set; }

        public virtual ICollection<Institute> Institutes { get; set; }
    }
}

