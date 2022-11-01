namespace PraeceptorCQRS.Contracts.Entities.Class
{
    public record ClassResponse(
        Guid Id,

        string Code,
        string Name,
        int Practice,
        int Theory,
        int PR,
        Guid InstituteId,
        Guid TypeId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

