using MediatR;
using ErrorOr;

using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public record CreateHoldingVariableXCommand(
        string GroupName,
        Guid GroupId,
        string VariableName,
        string? Value
        ) : IRequest<ErrorOr<VariableResultX>>;
}
