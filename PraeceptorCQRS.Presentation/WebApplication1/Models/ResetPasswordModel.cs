using System.ComponentModel.DataAnnotations;

namespace IdentitiServer.Api.Models
{
    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
