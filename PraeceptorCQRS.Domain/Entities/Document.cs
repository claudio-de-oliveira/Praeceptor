using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Document : BaseAuditableEntity
    {
        public Document(Guid id)
            : base(id)
        { /* Nothing more todo */}

        public static Document Create(string title, string? text, Guid instituteId, DateTime created, string? createdBy)
            => new(Guid.NewGuid())
            {
                Title = title,
                Text = text,
                InstituteId = instituteId,
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        [Required, MaxLength(1024)]
        public string Title { get; set; } = null!;

        public string? Text { get; set; }

        public Guid InstituteId { get; set; }

        [Required, ForeignKey("InstituteId")]
        public virtual Institute Institute { get; set; } = null!;
    }
}