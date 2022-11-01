using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.AxisType.Commands.CreateCommand
{
    public record CreateAxisTypeCommand(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<AxisTypeResult>>;
}
