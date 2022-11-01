namespace Administrative.App.Models
{
    public class InstituteModel : Entity
    {
        public string Acronym { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public Guid HoldingId { get; set; }

        public string Administrator { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
