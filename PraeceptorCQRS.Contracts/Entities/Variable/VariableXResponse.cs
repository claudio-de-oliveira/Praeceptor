namespace PraeceptorCQRS.Contracts.Entities.Variable
{
    public record VariableXResponse(
        Guid Id,
        string GroupName,
        Guid GroupId,
        string? Curriculum,
        string VariableName,
        string? Value,
        bool IsDeletable
        );
}
