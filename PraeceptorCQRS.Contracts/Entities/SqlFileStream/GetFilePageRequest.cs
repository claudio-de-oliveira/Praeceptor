namespace PraeceptorCQRS.Contracts.Entities.SqlFileStream
{
    public record GetFilePageRequest(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? NameFilter,
        string? TitleFilter,
        string? SourceFilter,
        string? DescriptionFilter,
        string? ContentTypeFilter,
        string? DateCreatedFilter
        );
}