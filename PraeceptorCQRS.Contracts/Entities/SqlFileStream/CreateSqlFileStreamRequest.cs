namespace PraeceptorCQRS.Contracts.Entities.DocumentTemplate
{
    public record CreateSqlFileStreamRequest(
        string Name,
        string? Title,
        string? Source,
        string? Description,
        byte[] Data,
        string ContentType,
        Guid InstituteId
    );
}

