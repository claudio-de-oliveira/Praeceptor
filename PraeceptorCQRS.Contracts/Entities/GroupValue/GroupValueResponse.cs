namespace PraeceptorCQRS.Contracts.Entities.GroupValue
{
    public record GroupValueResponse(
        Guid Id,
        string Value,
        Guid GroupId
        );
}
