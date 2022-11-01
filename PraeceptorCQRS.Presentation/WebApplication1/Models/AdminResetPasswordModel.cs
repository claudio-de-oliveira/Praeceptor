using System.ComponentModel.DataAnnotations;

namespace IdentitiServer.Api.Models
{
    public class AdminResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não são iguais.")]
        public string ConfirmPassword { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
