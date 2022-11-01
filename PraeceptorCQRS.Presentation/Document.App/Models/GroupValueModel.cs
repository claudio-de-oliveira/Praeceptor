namespace Document.App.Models
{
    public class GroupValueModel
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string Value { get; set; } = default!;
    }
}
