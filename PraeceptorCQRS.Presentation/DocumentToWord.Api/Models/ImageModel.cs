namespace DocumentToWord.Api.Models
{
    public class ImageModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string? Title { get; set; }
        public string? Source { get; set; }
        public string? Description { get; set; }
        public byte[] Data { get; set; } = default!;
        public int InstituteId { get; set; }
    }
}
