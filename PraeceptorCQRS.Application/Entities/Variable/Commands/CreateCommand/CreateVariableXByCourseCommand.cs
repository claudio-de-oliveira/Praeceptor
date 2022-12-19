using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public record CreateVariableXByCourseCommand(
        string GroupName,
        Guid GroupId,
        int? Curriculum,
        string VariableName,
        string? Value
        ) : IRequest<ErrorOr<VariableResultX>>;
}
