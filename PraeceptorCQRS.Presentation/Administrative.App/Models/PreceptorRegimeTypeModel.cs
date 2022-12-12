namespace Administrative.App.Models
{
    public class PreceptorRegimeTypeModel : AbstractTypeModel
    {
        public string Code3 { get; set; } = null!;
        public Guid InstituteId { get; set; }
    }
}
