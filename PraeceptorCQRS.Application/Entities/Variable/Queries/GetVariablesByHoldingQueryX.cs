using MediatR;
using ErrorOr;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public record GetVariablesByHoldingQueryX(
        Guid Id
        ) : IRequest<ErrorOr<VariableXListResult>>;
}
