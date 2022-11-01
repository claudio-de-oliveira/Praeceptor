namespace PraeceptorCQRS.Contracts.Entities.VariableValue
{
    public record UpdateVariableValueRequest(
        Guid Id,
        string Value
        );
}
