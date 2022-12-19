namespace PraeceptorCQRS.Contracts.Entities.Variable
{
    public record CreateVariableXRequest(
        string GroupName,
        Guid GroupId,
        int? Curriculum,
        string VariableName,
        string? Value
        );
}
