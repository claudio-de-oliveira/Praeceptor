namespace UserManager.App.Models
{
    public class UserModel
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string? PhoneNumber { get; set; }

        public bool IsEnabled { get; set; }
        public char Gender { get; set; }
        public string? HoldingId { get; set; }
        public string? InstituteId { get; set; }
        public string? CourseId { get; set; }
    }
}
