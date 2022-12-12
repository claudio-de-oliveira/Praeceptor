namespace PraeceptorCQRS.Contracts.Entities.PreceptorRoleType
{
    public record UpdatePreceptorRoleTypeRequest(
        Guid Id,
        string Code,
        string Code3
    );
}
