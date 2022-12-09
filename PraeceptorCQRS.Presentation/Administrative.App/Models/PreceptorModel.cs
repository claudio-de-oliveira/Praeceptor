namespace Administrative.App.Models
{
    public class PreceptorModel : Entity
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Image { get; set; }
        public PreceptorDegreeTypeModel DegreeType { get; set; } = default!;
        public Guid DegreeTypeId { get; set; }
        public PreceptorRegimeTypeModel RegimeType { get; set; } = default!;
        public Guid RegimeTypeId { get; set; }
        public Guid InstituteId { get; set; }
    }
}
