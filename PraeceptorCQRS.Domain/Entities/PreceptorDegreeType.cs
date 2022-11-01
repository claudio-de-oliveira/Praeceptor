using PraeceptorCQRS.Domain.Base;
using PraeceptorCQRS.Domain.Values;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class PreceptorDegreeType : BaseAuditableEntity, IComparable
    {
        public PreceptorDegreeType(Guid id)
            : base(id)
        {
            Preceptors = new HashSet<Preceptor>();
        }

        public static PreceptorDegreeType Create(
            string code,
            Guid instituteId,
            DateTime created,
            string? createdBy
            )
            => new(Guid.Empty)
            {
                Code = code,
                InstituteId = instituteId,
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        public int CompareTo(object? obj)
        {
            if (obj is not null && obj is PreceptorDegreeType tp)
                return Code.CompareTo(tp.Code);
            return 0;
        }

        [Required, MaxLength(20)]
        public string Code { get; set; } = null!;

        public Guid InstituteId { get; set; }
        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = null!;

        public virtual ICollection<Preceptor> Preceptors { get; set; } = null!;
    }
}

