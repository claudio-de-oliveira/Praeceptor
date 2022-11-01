using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.Entities
{
    public class DocumentTemplate : BaseAuditableEntity
    {
        public DocumentTemplate(Guid id)
            : base(id)
        { /* Nothing more todo */}

        public static DocumentTemplate Create(
            DateTime created,
            string? createdBy
            )
            =>
            new(Guid.Empty)
            {
                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };
    }
}

