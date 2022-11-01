namespace PraeceptorCQRS.Contracts.Entities.ClassType
{
    public record ClassTypeResponse(
        Guid Id,
        string Code,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

