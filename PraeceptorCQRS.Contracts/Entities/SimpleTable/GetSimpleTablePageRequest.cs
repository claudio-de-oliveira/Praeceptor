namespace PraeceptorCQRS.Contracts.Entities.SimpleTable;

public record GetSimpleTablePageRequest(
    Guid InstituteId,
    int Start,
    int Count,
    string? Sort,
    bool Ascending,
    string? CodeFilter,
    string? TitleFilter,
    string? HeaderFilter,
    string? CreatedByFilter,
    string? CreatedFilter,
    string? LastModifiedFilter,
    string? LastModifiedByFilter
    );