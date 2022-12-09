namespace PraeceptorCQRS.Contracts.Entities.SqlDocxStream
{
    public record GetDocxPageRequest(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? TitleFilter,
        string? DescriptionFilter,
        string? ContentTypeFilter,
        string? DateCreatedFilter,
        string? CreatedByFilter
        );
}