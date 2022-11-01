namespace PraeceptorCQRS.Contracts.Entities.Preceptor
{
    public record GetPreceptorPageRequest(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? CodeFilter,
        string? NameFilter,
        string? EmailFilter,
        Guid? DegreeTypeFilter,
        Guid? RegimeTypeFilter,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        );
}
