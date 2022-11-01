namespace PraeceptorCQRS.Contracts.Entities.AxisType
{
    public record AxisTypeResponse(
        Guid Id,
        string Code,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}
