using System.ComponentModel.DataAnnotations;

namespace IdentitiServer.Api.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = default!;
        [EmailAddress]
        public string Email { get; set; } = default!;
        public char Gender { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = default!;
        public string? HoldingId { get; set; }
        public string? InstituteId { get; set; }
        public string? CourseId { get; set; }
        public string? PhoneNumber { get; set; }
        // public string Role { get; set; } = default!;
    }
}
