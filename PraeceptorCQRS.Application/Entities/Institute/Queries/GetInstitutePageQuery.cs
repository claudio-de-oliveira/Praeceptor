using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Institute.Common;

namespace PraeceptorCQRS.Application.Entities.Institute.Queries
{
    public record GetInstitutePageQuery(
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
        ) : IRequest<ErrorOr<InstitutePageResult>>;
}
