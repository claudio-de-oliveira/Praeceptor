using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;

namespace PraeceptorCQRS.Application.Entities.Group.Commands.CreateCommand
{
    public record CreateGroupCommand(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<GroupResult>>;
}
