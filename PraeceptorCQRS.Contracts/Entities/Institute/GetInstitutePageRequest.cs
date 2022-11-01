namespace PraeceptorCQRS.Contracts.Entities.Institute
{
    public record GetInstitutePageRequest(
        Guid HoldingId,
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
