namespace PraeceptorCQRS.Contracts.Entities.AxisType
{
    public record GetAxisTypePageRequest(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? CodeFilter,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        );
}
