using PraeceptorCQRS.Domain.Base;

using System.ComponentModel.DataAnnotations;

namespace PraeceptorCQRS.Domain.Entities
{
    public class SqlFileStream : BaseEntity
    {
        public SqlFileStream(Guid id)
            : base(id)
        { /* Nothing more todo */ }

        [Required, MaxLength(250)]
        public string Name { get; set; } = default!;
        [MaxLength(4000)]
        public string? Title { get; set; }
        [MaxLength(4000)]
        public string? Source { get; set; }
        [MaxLength(4000)]
        public string? Description { get; set; }
        [Required]
        public byte[] Data { get; set; } = default!;
        public Guid InstituteId { get; set; }
        public string ContentType { get; set; } = default!;
        public DateTime DateCreated { get; set; }
    }
}
