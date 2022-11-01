using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;

namespace PraeceptorCQRS.Application.Entities.Component.Commands.DeleteCommand
{
    public record DeleteComponentCommand(
        Guid CourseId,
        int Curriculum,
        Guid ClassId
        ) : IRequest<ErrorOr<ComponentResult>>;
}
