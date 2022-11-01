namespace Administrative.App.Models
{
    public class ClassModel : Entity
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Practice { get; set; }
        public int Theory { get; set; }
        public int PR { get; set; }
        public Guid InstituteId { get; set; }
        public Guid TypeId { get; set; }
    }
}
