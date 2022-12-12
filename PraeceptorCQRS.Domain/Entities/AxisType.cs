using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class AxisType : BaseAuditableEntity, IComparable
    {
        public AxisType(Guid id)
            : base(id)
        {
            Components = new HashSet<Component>();
        }

        public static AxisType Create(
            string code,
            string code3,
            Guid instituteId,
            DateTime created,
            string? createdBy
            )
            => new(Guid.NewGuid())
            {
                Code = code,
                Code3 = code3,
                InstituteId = instituteId,
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        public int CompareTo(object? obj)
        {
            if (obj is not null && obj is AxisType tp)
                return Code.CompareTo(tp.Code);
            return 0;
        }

        [Required, MaxLength(20)]
        public string Code { get; set; } = null!;

        [Required, MaxLength(3)]
        public string Code3 { get; set; } = null!;

        public Guid InstituteId { get; set; }

        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = null!;

        public virtual ICollection<Component> Components { get; set; }
    }
}