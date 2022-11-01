using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.DeleteCommand
{
    public record DeleteGroupValueCommand(
        Guid Id
        ) : IRequest<ErrorOr<GroupValueResult>>;
}
