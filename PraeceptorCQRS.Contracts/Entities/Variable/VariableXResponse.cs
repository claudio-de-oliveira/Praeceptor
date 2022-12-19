namespace PraeceptorCQRS.Contracts.Entities.Variable
{
    public record VariableXResponse(
        Guid Id,
        string GroupName,
        Guid GroupId,
        int? Curriculum,
        string VariableName,
        string? Value,
        bool IsDeletable
        );
}
