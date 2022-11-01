namespace DocumentToWord.Api.Models
{
    public class TableModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Title { get; set; }
        public string? Source { get; set; }
        public string? Description { get; set; }
        public string Text { get; set; } = default!;
        public int InstituteId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
