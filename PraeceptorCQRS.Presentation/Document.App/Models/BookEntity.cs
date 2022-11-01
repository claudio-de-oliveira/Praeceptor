using System.ComponentModel.DataAnnotations;

namespace Document.App.Models
{
    public class BookEntity : Entity
    {
        [Required(ErrorMessage = "O título é obrigatório."), MaxLength(1024, ErrorMessage = "O número máximo de caracteres para o título é 1024")]
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public Guid InstituteId { get; set; }
    }
}
