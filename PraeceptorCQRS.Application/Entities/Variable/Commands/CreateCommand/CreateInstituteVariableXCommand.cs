using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public record CreateInstituteVariableXCommand(
        string GroupName,
        Guid GroupId,
        string VariableName,
        string? Value
        ) : IRequest<ErrorOr<VariableResultX>>;
}
