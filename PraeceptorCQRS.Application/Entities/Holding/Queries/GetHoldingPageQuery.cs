using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Holding.Common;

namespace PraeceptorCQRS.Application.Entities.Holding.Queries
{
    public record GetHoldingPageQuery(
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
        ) : IRequest<ErrorOr<HoldingPageResult>>;
}
