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
        bool Optative,
        Guid AxisTypeId
        ) : IRequest<ErrorOr<ComponentResult>>;
}
