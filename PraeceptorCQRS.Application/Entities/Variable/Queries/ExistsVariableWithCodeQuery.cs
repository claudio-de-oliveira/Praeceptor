using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public record ExistsVariableWithCodeQuery(
        Guid GroupId,
        string Code
        ) : IRequest<ErrorOr<VariableExistResult>>;
}
