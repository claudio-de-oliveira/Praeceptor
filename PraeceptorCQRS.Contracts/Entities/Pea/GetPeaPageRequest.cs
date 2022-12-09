namespace PraeceptorCQRS.Contracts.Entities.Pea
{
    public record GetPeaPageRequest(
        Guid ClassId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        );
}