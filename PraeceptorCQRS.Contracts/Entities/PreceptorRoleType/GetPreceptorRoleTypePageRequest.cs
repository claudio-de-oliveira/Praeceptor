namespace PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;

public record GetPreceptorRoleTypePageRequest(
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
