using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.Component.Commands.CreateCommand
{
    public record CreateComponentCommand(
        Guid CourseId,
        int Curriculum,
        int Season,
        Guid ClassId,
        bool Optative,
        Guid AxisTypeId
        ) : IRequest<ErrorOr<ComponentResult>>;
}
