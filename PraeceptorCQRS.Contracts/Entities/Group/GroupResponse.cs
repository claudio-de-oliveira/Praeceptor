namespace PraeceptorCQRS.Contracts.Entities.Group
{
    public record GroupResponse(
        Guid Id,
        string Code,
        Guid InstituteId
        );
}
