namespace Document.App.Models
{
    public class SimpleTableModel : Entity
    {
        public string Code { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Header { get; set; } = default!;
        public string Rows { get; set; } = default!;
        public string? Footer { get; set; } = default!;
        public Guid InstituteId { get; set; }
    }
}