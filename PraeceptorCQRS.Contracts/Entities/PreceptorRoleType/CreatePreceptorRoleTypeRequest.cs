namespace PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;

public record CreatePreceptorRoleTypeRequest(
    string Code,
    Guid InstituteId
);
