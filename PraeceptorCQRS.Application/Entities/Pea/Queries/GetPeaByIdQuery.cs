using ErrorOr;
using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;

namespace PraeceptorCQRS.Application.Entities.Pea.Queries
{
    public record GetPeaByIdQuery(Guid Id) : IRequest<ErrorOr<PeaResult>>;
}
