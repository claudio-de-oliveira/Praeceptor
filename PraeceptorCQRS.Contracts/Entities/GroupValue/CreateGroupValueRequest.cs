namespace PraeceptorCQRS.Contracts.Entities.GroupValue
{
    public record CreateGroupValueRequest(
        Guid GroupId,
        string Value
        );
}
