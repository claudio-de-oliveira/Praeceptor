using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PraeceptorCQRS.Domain.Base;
using PraeceptorCQRS.Domain.Values;
using System.Runtime.CompilerServices;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Pea : BaseAuditableEntity
    {
        public Pea(Guid id) 
            : base(id)
        { /* Nothing more todo */ }

        public static Pea Create(string text, Guid classId, DateTime created, string? createdBy)
            => new(Guid.NewGuid())
            {
                Text = text,
                ClassId = classId,
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        [Required]
        public string Text { get; set; } = default!;

        #region Entity Framework Relationships
        public Guid ClassId { get; set; }
        [Required, ForeignKey("ClassId")]
        public virtual Class Class { get; set; } = default!;
        #endregion
    }
}
