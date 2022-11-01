namespace PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType
{
    public record GetPreceptorDegreeTypePageRequest(
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
