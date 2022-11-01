namespace Document.App.Models
{
    public class VariableValueModel
    {
        public Guid Id { get; set; }
        public Guid GroupValueId { get; set; }
        public Guid VariableId { get; set; }
        public string Value { get; set; } = default!;
    }
}
