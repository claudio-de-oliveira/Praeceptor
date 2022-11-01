using PraeceptorCQRS.Domain.Base;
using PraeceptorCQRS.Domain.Values;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Course : BaseAuditableEntity
    {
        public Course(Guid id)
            : base(id)
        {
            Components = new HashSet<Component>();
        }

        public static Course Create(
            string code,
            string name,
            Guid? ceo,
            int ac,
            int seasons,
            int minimumWorkload,
            string? telephone,
            string? email,
            string? image,
            Guid instituteId,
            DateTime created,
            string? createdBy
            )
            =>
            new(Guid.Empty)
            {
                Code = code,
                Name = name,
                CEO = ceo,
                AC = ac,
                NumberOfSeasons = seasons,
                MinimumWorkload = minimumWorkload,
                Telephone = telephone,
                Email = email,
                Image = image,
                InstituteId = instituteId,
                // ...
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        [Required, MaxLength(250)]
        public string Code { get; set; } = default!;
        [Required, MaxLength(250)]
        public string Name { get; set; } = default!;
        public Guid? CEO { get; set; } = default!;
        public int AC { get; set; }
        public int NumberOfSeasons { get; set; }
        public int MinimumWorkload { get; set; }
        [MaxLength(100)]
        public string? Telephone { get; set; } = default!;
        [MaxLength(250)]
        public string? Email { get; set; } = default!;
        [MaxLength(4000)]
        public string? Image { get; set; } = default!;

        public Guid InstituteId { get; set; }
        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = default!;

        public virtual ICollection<Component> Components { get; set; } = default!;
    }
}

