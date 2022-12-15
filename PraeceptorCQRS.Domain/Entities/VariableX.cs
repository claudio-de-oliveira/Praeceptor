namespace PraeceptorCQRS.Domain.Entities
{
    public class VariableX
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; } = default!;
        public Guid GroupId { get; set; }
        public string? Curriculum { get; set; }
        public string VariableName { get; set; } = default!;
        public string? Value { get; set; }
        public bool IsDeletable { get; set; }
    }
}
