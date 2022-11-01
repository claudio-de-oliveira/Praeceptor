using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.AxisType.Common;

namespace PraeceptorCQRS.Application.Entities.AxisType.Commands.DeleteCommand
{
    public record DeleteAxisTypeCommand(
        Guid Id
        ) : IRequest<ErrorOr<AxisTypeResult>>;
}
