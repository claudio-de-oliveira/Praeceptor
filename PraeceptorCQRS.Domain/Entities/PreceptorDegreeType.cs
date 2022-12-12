using PraeceptorCQRS.Domain.Base;

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
            string code3,
            bool latoSensu,
            bool strictoSensu,
            Guid instituteId,
            DateTime created,
            string? createdBy
            )
            => new(Guid.Empty)
            {
                Code = code,
                Code3 = code3,
                StrictoSensu = strictoSensu,
                LatoSensu = latoSensu,
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
        [Required, MaxLength(3)]
        public string Code3 { get; set; } = null!;

        public bool StrictoSensu { get; set; }
        public bool LatoSensu { get; set; }

        public Guid InstituteId { get; set; }

        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = null!;

        public virtual ICollection<Preceptor> Preceptors { get; set; } = null!;
    }
}