namespace PraeceptorCQRS.Contracts.Entities.Variable
{
    public record UpdateVariableXRequest(
        Guid Id,
        string? Value
        );
}
