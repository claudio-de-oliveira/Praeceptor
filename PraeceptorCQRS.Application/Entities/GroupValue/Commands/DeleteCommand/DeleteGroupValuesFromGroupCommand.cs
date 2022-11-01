using ErrorOr;
using MediatR;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.DeleteCommand
{
    public record class DeleteGroupValuesFromGroupCommand(
        Guid GroupId
        ) : IRequest<ErrorOr<bool>>;
}
