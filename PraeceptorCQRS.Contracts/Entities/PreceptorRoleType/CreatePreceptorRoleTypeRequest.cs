namespace PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;

public record CreatePreceptorRoleTypeRequest(
    string Code,
    string Code3,
    Guid InstituteId
);
