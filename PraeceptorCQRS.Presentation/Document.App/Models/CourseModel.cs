namespace Document.App.Models
{
    public class CourseModel : Entity
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int AC { get; set; }
        public int NumberOfSeasons { get; set; }
        public int MinimumWorkload { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public Guid InstituteId { get; set; }
    }
}