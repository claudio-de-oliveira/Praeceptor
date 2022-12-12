namespace PraeceptorCQRS.Contracts.Entities.ClassType
{
    public record ClassTypeResponse(
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

