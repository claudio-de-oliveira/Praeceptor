namespace PraeceptorCQRS.Contracts.Entities.Variable
{
    public record CreateVariableXRequest(
        string GroupName,
        Guid GroupId,
        string? Curriculum,
        string VariableName,
        string? Value
        );
}
