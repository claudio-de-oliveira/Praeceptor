using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;

namespace PraeceptorCQRS.Application.Entities.Group.Commands.DeleteCommand
{
    public record DeleteGroupCommand(
        Guid Id
        ) : IRequest<ErrorOr<GroupResult>>;
}
