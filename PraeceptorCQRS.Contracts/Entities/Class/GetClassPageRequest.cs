namespace PraeceptorCQRS.Contracts.Entities.Class
{
    public record GetClassPageRequest(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? Code,
        string? Name,
        int Practice,
        int Theory,
        int PR,
        Guid TypeId,
        string? CreatedFilter,
        string? CreatedByFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        );
}
