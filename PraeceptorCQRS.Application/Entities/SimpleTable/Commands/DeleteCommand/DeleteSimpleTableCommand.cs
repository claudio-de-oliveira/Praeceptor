using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Commands.DeleteCommand
{
    public record DeleteSimpleTableCommand(
        Guid Id
        ) : IRequest<ErrorOr<SimpleTableResult>>;
}