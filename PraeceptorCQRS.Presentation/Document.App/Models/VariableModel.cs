namespace Document.App.Models
{
    public class VariableModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public Guid GroupId { get; set; }

        public VariableValueModel? VariableValue { get; set; }
    }
}
