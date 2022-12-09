using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Class : BaseAuditableEntity
    {
        public Class(Guid id)
            : base(id)
        {
            Components = new HashSet<Component>();
            Peas = new HashSet<Pea>();
        }

        public static Class Create(
            string code,
            string name,
            int practice,
            int theory,
            int pr,
            Guid typeId,
            Guid instituteId,
            bool hasPlanner,
            DateTime created,
            string? createdBy
            )
            => new(Guid.NewGuid())
            {
                Code = code,
                Name = name,
                Practice = practice,
                Theory = theory,
                PR = pr,
                InstituteId = instituteId,
                TypeId = typeId,
                HasPlanner = hasPlanner,
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        [Required, MaxLength(20)]
        public string Code { get; set; } = null!;

        [Required, MaxLength(250)]
        public string Name { get; set; } = null!;

        public int Practice { get; set; }
        public int Theory { get; set; }
        public int PR { get; set; }
        public bool HasPlanner { get; set; }

        public Guid InstituteId { get; set; }

        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = null!;

        public Guid TypeId { get; set; }

        [Required, ForeignKey("TypeId")]
        public virtual ClassType Type { get; set; } = null!;

        public virtual ICollection<Pea> Peas { get; set; }
        public virtual ICollection<Component> Components { get; set; }
    }
}