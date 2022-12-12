using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class ClassType : BaseAuditableEntity, IComparable
    {
        public ClassType(Guid id)
            : base(id)
        {
            Classes = new HashSet<Class>();
        }

        public static ClassType Create(
            string code,
            string code3,
            Guid instituteId,
            bool isRemote,
            int durationInMinutes,
            DateTime created,
            string? createdBy
            )
            => new(Guid.Empty)
            {
                Code = code,
                Code3 = code3,
                InstituteId = instituteId,
                IsRemote = isRemote,
                DurationInMinutes = durationInMinutes,
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        public int CompareTo(object? obj)
        {
            if (obj is not null && obj is ClassType tp)
                return Code.CompareTo(tp.Code);
            return 0;
        }

        [Required, MaxLength(20)]
        public string Code { get; set; } = null!;
        [Required, MaxLength(3)]
        public string Code3 { get; set; } = null!;

        public bool IsRemote { get; set; }
        public bool IsTCC { get; set; }
        public bool IsEstagio { get; set; }
        public int DurationInMinutes { get; set; }

        public Guid InstituteId { get; set; }

        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; }
    }
}