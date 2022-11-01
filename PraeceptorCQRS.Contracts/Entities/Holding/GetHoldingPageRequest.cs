namespace PraeceptorCQRS.Contracts.Entities.Holding
{
    public record GetHoldingPageRequest(
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? AcronymFilter,
        string? NameFilter,
        string? AddressFilter,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        );
}
