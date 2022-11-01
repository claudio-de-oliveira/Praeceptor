namespace PraeceptorCQRS.Contracts.Entities.Variable
{
    public record VariableResponse(
        Guid Id,
        string Code,
        Guid GroupId
        );
}
