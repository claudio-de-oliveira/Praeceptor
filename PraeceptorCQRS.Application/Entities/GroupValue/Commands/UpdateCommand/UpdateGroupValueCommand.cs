using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.UpdateCommand
{
    public record UpdateGroupValueCommand(
        Guid Id,
        string Value
        ) : IRequest<ErrorOr<GroupValueResult>>;
}
