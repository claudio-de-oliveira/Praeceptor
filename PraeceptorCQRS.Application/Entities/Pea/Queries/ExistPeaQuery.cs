using ErrorOr;

using MediatR;

namespace PraeceptorCQRS.Application.Entities.Pea.Queries
{
    public record ExistPeaQuery(string Code) : IRequest<ErrorOr<bool>>;
}
