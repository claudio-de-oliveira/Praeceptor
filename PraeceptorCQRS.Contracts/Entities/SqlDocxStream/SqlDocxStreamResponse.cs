namespace PraeceptorCQRS.Contracts.Entities.SqlDocxStream
{
    public record DocxResponse(
        Guid Id,
        string Title,
        string Description,
        byte[] Data,
        Guid InstituteId,
        string ContentType,
        DateTime DateCreated,
        string CreatedBy
    );
}