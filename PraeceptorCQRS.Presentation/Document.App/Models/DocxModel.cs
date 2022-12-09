namespace Document.App.Models
{
    public class DocxModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public byte[] Data { get; set; } = default!;
        public Guid InstituteId { get; set; }
        public string ContentType { get; set; } = default!;
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = default!;
    }
}