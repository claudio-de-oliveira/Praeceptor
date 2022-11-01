using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public record GetVariableByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<VariableResult>>;
}
