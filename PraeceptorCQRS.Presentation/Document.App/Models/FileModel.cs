namespace Document.App.Models
{
    public class FileModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Title { get; set; }
        public string? Source { get; set; }
        public string? Description { get; set; }
        public byte[] Data { get; set; } = default!;
        public Guid InstituteId { get; set; }
        public string ContentType { get; set; } = default!;
        public DateTime DateCreated { get; set; }
    }
}
