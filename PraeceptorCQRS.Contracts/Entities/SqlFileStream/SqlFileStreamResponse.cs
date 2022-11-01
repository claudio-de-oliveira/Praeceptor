namespace PraeceptorCQRS.Contracts.Entities.DocumentTemplate
{
    public record SqlFileStreamResponse(
        Guid Id,
        string Name,
        string Title,
        string Source,
        string Description,
        byte[] Data,
        Guid InstituteId,
        string ContentType,
        DateTime DateCreated
   );
}

