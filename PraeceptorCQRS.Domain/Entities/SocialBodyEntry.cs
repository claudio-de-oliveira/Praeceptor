using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraeceptorCQRS.Domain.Entities
{
    public class SocialBodyEntry
    {
        public Guid PreceptorId { get; set; }
        [Required, ForeignKey("PreceptorId")]
        public virtual Preceptor Preceptor { get; set; } = default!;

        public Guid CourseId { get; set; }
        [Required, ForeignKey("CourseId")]
        public virtual Course Course { get; set; } = default!;

        public Guid RoleId { get; set; }
        [Required, ForeignKey("RoleId")]
        public virtual PreceptorRoleType Role { get; set; } = default!;
    }
}
