namespace PraeceptorCQRS.Contracts.Entities.Variable
{
    public record CreateVariableRequest(
        Guid GroupId,
        string Code
        );
}
