
using PraeceptorCQRS.Domain.Base;
using PraeceptorCQRS.Domain.Values;

using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Node : BaseAuditableEntity
    {
        public Node(Guid id)
            : base(id)
        { /* Nothing more todo */}

        public static Node Create(Guid firstEntityId, Guid documentId, Guid secondEntityId, DateTime created, string? createdBy)
            => new(Guid.Empty)
            {
                PreviousNodeId = null,              // Lista
                NextNodeId = null,                  // Lista

                FirstEntityId = firstEntityId,      // Pai
                DocumentId = documentId,
                SecondEntityId = secondEntityId,    // Filho

                Created = created,
                CreatedBy = createdBy,
                LastModified = created,
                LastModifiedBy = createdBy
            };

        public Guid? PreviousNodeId { get; set; }
        [ForeignKey("PreviousNodeId")]
        public virtual Node PreviousNode { get; set; } = null!;
        public Guid? NextNodeId { get; set; }
        [ForeignKey("NextNodeId")]
        public virtual Node NextNode { get; set; } = null!;

        public Guid FirstEntityId { get; set; }
        // *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
        // Para não compartilhar a estrutura de documentos
        public Guid DocumentId { get; set; }
        public Guid SecondEntityId { get; set; }
    }
}
