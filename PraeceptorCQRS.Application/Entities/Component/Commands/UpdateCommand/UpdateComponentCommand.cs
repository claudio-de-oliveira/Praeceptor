using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;

namespace PraeceptorCQRS.Application.Entities.Component.Commands.UpdateCommand
{
    public record UpdateComponentCommand(
        Guid CourseId,
        int Curriculum,
        int Season,
        Guid ClassId,
        Guid AxisTypeId,
        bool Optative
        ) : IRequest<ErrorOr<ComponentResult>>;
}
