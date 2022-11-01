using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class Component
    {
        public bool Optative { get; set; }

        [Required]
        public int Season { get; set; }
        [Required]
        public int Curriculum { get; set; }

        #region Entity Framework Relationships
        public Guid AxisTypeId { get; set; }
        [Required, ForeignKey("AxisTypeId")]
        public AxisType Axis { get; set; } = default!;
        public Guid ClassId { get; set; }
        [Required, ForeignKey("ClassId")]
        public virtual Class Class { get; set; } = default!;
        public Guid CourseId { get; set; }
        [Required, ForeignKey("CourseId")]
        public virtual Course Course { get; set; } = default!;
        #endregion
    }
}
