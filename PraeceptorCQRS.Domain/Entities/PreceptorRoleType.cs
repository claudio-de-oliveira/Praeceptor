using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PraeceptorCQRS.Domain.Entities
{
    public class PreceptorRoleType : BaseAuditableEntity, IComparable
    {
        public PreceptorRoleType(Guid id)
            : base(id)
        {
            CourseSocialBodyEntries = new HashSet<SocialBodyEntry>();
        }

        public static PreceptorRoleType Create(
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
            if (obj is not null && obj is PreceptorRoleType tp)
                return Code.CompareTo(tp.Code);
            return 0;
        }

        [Required, MaxLength(20)]
        public string Code { get; set; } = null!;

        public Guid InstituteId { get; set; }
        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = null!;

        public virtual ICollection<SocialBodyEntry> CourseSocialBodyEntries { get; set; } = null!;
    }
}
