namespace Administrative.App.Models
{
    public class SocialBodyEntryModel
    {
        public CourseModel Course {get; set; } = default!;
        public PreceptorModel Preceptor {get; set; } = default!;
        public PreceptorRoleTypeModel Role { get; set; } = default!;
    }
}
