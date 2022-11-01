namespace PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType
{
    public record GetPreceptorRegimeTypePageRequest(
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
