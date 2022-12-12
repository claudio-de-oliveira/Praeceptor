using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.AxisType.Common;

namespace PraeceptorCQRS.Application.Entities.AxisType.Commands.UpdateCommand
{
    public record UpdateAxisTypeCommand(
        Guid Id,
        string Code,
        string Code3
        ) : IRequest<ErrorOr<AxisTypeResult>>;
}
