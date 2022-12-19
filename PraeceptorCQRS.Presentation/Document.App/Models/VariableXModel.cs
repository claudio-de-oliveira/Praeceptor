namespace Document.App.Models
{
    public class VariableXModel
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; } = default!;
        public Guid GroupId { get; set; }
        public string VariableName { get; set; } = default!;
        public int? Curriculum { get; set; }
        public string Value { get; set; } = default!;
        public bool IsDeletable { get; set; }
    }
}
