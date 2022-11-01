namespace Administrative.App.Models
{
    public class CourseModel : Entity
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Guid? CEO { get; set; }
        public string? CEO_Email { get; set; }
        public int AC { get; set; }
        public int NumberOfSeasons { get; set; }
        public int MinimumWorkload { get; set; }
        public string? Telephone { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Image { get; set; } = null!;
        public Guid InstituteId { get; set; }
    }
}
