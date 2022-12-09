namespace Administrative.App.Models
{
    public class PreceptorDegreeTypeModel : AbstractTypeModel
    {
        public Guid InstituteId { get; set; }
        public bool LatoSensu { get; set; }
        public bool StrictoSensu { get; set; }
    }
}