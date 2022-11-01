namespace Document.App.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public Guid InstituteId { get; set; }

        public List<VariableModel> Variables { get; set; } = new();
        public Dictionary<Guid, string> GroupValues { get; set; } = new();
        public Guid GroupValueSelectedId { get; set; } = Guid.Empty;
    }
}
