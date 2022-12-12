namespace Administrative.App.Models
{
    public class PreceptorDegreeTypeModel : AbstractTypeModel
    {
        public string Code3 { get; set; } = null!;
        public Guid InstituteId { get; set; }
        public bool LatoSensu { get; set; }
        public bool StrictoSensu { get; set; }
    }
}