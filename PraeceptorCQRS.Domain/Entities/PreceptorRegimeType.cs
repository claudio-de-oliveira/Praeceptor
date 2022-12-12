using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class PreceptorRegimeType : BaseAuditableEntity, IComparable
    {
        public PreceptorRegimeType(Guid id)
            : base(id)
        {
            Preceptors = new HashSet<Preceptor>();
        }

        public static PreceptorRegimeType Create(
            string code,
            string code3,
            Guid instituteId,
            DateTime created,
            string? createdBy
            )
            => new(Guid.Empty)
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
            if (obj is not null && obj is PreceptorRegimeType tp)
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

        public virtual ICollection<Preceptor> Preceptors { get; set; } = null!;
    }
}

