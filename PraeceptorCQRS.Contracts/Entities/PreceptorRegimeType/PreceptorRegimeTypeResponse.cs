namespace PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType
{
    public record PreceptorRegimeTypeResponse(
        Guid Id,

        string Code,
        string Code3,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

