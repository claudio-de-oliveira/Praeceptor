using System.ComponentModel.DataAnnotations;

namespace IdentitiServer.Api.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
    }
}
