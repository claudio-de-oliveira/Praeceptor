
using System.Reflection;

namespace PraeceptorCQRS.Domain.Entities
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public bool IsEnabled { get; set; }
        public char Gender { get; set; }
        public string? HoldingId { get; set; }
        public string? InstituteId { get; set; }
        public string? CourseId { get; set; }

        public static PropertyInfo[] GetProperties()
        {
            throw new NotImplementedException();
        }
    }
}
