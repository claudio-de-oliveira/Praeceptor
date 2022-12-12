namespace PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;

public record PreceptorRoleTypeResponse(
    Guid Id,

    string Code,
    string Code3,
    Guid InstituteId,

    DateTime Created,
    string? CreatedBy,
    DateTime? LastModified,
    string? LastModifiedBy
);
