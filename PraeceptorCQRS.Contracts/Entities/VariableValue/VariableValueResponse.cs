namespace PraeceptorCQRS.Contracts.Entities.VariableValue
{
    public record VariableValueResponse(
        Guid Id,
        Guid GroupValueId,
        Guid VariableId,
        string Value
        );
}
