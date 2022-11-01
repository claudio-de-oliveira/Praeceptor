using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Queries
{
    public record GetVariableValueByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<VariableValueResult>>;
}
