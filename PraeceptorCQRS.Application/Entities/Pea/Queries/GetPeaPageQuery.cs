using ErrorOr;
using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;

namespace PraeceptorCQRS.Application.Entities.Pea.Queries
{
    public record GetPeaPageQuery(
        Guid ClassId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        ) : IRequest<ErrorOr<PeaPageResult>>;
}
