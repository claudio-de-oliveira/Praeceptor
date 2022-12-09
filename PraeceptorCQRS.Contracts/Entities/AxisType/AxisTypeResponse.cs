namespace PraeceptorCQRS.Contracts.Entities.AxisType
{
    public record AxisTypeResponse(
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