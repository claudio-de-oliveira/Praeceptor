namespace PraeceptorCQRS.Contracts.Entities.DocumentTemplate
{
    public record CreateFileRequest(
        string Name,
        string? Title,
        string? Source,
        string? Description,
        byte[] Data,
        string ContentType,
        Guid InstituteId
    );
}