namespace Administrative.App.Models
{
    public class ComponentModel
    {
        public bool Optative { get; set; }
        public int Season { get; set; }
        // public int Curriculum { get; set; }
        public Guid AxisTypeId { get; set; }
        public Guid ClassId { get; set; }
        // public Guid CourseId { get; set; }
        public AxisTypeModel Axis { get; set; } = default!;
        public ClassModel Class { get; set; } = default!;
    }
}