using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Queries
{
    public record GetVariableValueByVariableAndGroupValueQuery(
        Guid GroupValueId,
        Guid VariableId
        ) : IRequest<ErrorOr<VariableValueResult>>;
}
