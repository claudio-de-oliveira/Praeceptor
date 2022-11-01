namespace PraeceptorCQRS.Contracts.Entities.Group
{
    public record CreateGroupRequest(
        string Code,
        Guid InstituteId
        );
}
