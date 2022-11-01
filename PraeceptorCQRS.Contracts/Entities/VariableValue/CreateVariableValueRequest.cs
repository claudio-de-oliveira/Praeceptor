namespace PraeceptorCQRS.Contracts.Entities.VariableValue
{
    public record CreateVariableValueRequest(
        Guid GroupValueId,
        Guid VariableId,
        string Value
        );
}
