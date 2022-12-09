namespace PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;

public record PreceptorRoleTypeResponse(
    Guid Id,

    string Code,
    Guid InstituteId,

    DateTime Created,
    string? CreatedBy,
    DateTime? LastModified,
    string? LastModifiedBy
);
