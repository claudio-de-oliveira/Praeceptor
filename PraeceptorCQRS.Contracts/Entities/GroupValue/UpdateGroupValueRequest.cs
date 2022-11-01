namespace PraeceptorCQRS.Contracts.Entities.GroupValue
{
    public record UpdateGroupValueRequest(
        Guid Id,
        string Value
        );
}
