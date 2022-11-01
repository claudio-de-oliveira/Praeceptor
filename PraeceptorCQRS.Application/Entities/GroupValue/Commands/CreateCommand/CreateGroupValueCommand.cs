using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.CreateCommand
{
    public record CreateGroupValueCommand(
        Guid GroupId,
        string Value
        ) : IRequest<ErrorOr<GroupValueResult>>;
}
