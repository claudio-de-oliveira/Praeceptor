using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Preceptor : BaseAuditableEntity
    {
        public Preceptor(Guid id)
            : base(id)
        {
            CourseSocialBodyEntries = new HashSet<SocialBodyEntry>();
        }

        public static Preceptor Create(
            string code,
            string name,
            string email,
            string? image,
            Guid degreeTypeId,
            Guid regimeTypeId,
            Guid instituteId,
            DateTime created,
            string? createdBy
            )
            => new(Guid.Empty)
            {
                Code = code,
                Name = name,
                Email = email,
                Image = image,
                DegreeTypeId = degreeTypeId,
                RegimeTypeId = regimeTypeId,
                InstituteId = instituteId,
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        [Required, MaxLength(20)]
        public string Code { get; set; } = null!;
        [Required, MaxLength(250)]
        public string Name { get; set; } = null!;
        [MaxLength(250)]
        public string Email { get; set; } = null!;
        [MaxLength(2000)]
        public string? Image { get; set; }

        public Guid DegreeTypeId { get; set; }
        [Required, ForeignKey("DegreeTypeId")]
        public virtual PreceptorDegreeType DegreeType { get; set; } = default!;

        public Guid RegimeTypeId { get; set; }
        [Required, ForeignKey("RegimeTypeId")]
        public virtual PreceptorRegimeType RegimeType { get; set; } = default!;

        public Guid InstituteId { get; set; }
        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = null!;

        public virtual ICollection<SocialBodyEntry> CourseSocialBodyEntries { get; set; } = null!;
    }
}

