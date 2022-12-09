using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Class.Common;

namespace PraeceptorCQRS.Application.Entities.Class.Commands
{
    public record CreateClassCommand(
        string Code,
        string Name,
        int Practice,
        int Theory,
        int PR,
        Guid InstituteId,
        Guid TypeId,
        bool HasPlanner
        ) : IRequest<ErrorOr<ClassResult>>;
}