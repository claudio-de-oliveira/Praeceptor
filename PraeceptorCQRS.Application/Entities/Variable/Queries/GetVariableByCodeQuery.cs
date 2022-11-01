using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public record GetVariableByCodeQuery(
        Guid GroupId,
        string Code
        ) : IRequest<ErrorOr<VariableResult>>;
}
