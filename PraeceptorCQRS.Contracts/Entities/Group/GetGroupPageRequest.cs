namespace PraeceptorCQRS.Contracts.Entities.Group
{
    public record GetGroupPageRequest(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? Code
        );
}
