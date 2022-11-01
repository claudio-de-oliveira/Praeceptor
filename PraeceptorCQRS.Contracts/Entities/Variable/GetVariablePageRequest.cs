namespace PraeceptorCQRS.Contracts.Entities.Variable
{
    public record GetVariablePageRequest(
        Guid GroupId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? Code
    );
}
