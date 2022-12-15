using MediatR;
using ErrorOr;

using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.UpdateCommand
{
    public record UpdateVariableXByInstituteCommand(
        Guid Id,
        string? Value
        ) : IRequest<ErrorOr<VariableResultX>>;
}
