using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;

namespace PraeceptorCQRS.Domain.Entities
{
    public class SqlDocxStream : BaseEntity
    {
        public SqlDocxStream(Guid id)
            : base(id)
        { /* Nothing more todo */ }

        [MaxLength(4000)]
        public string? Title { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }

        [Required]
        public byte[] Data { get; set; } = default!;

        public Guid InstituteId { get; set; }
        public string ContentType { get; set; } = default!;
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = default!;
    }
}