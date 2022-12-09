using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Class.Common;

namespace PraeceptorCQRS.Application.Entities.Class.Commands
{
    public record UpdateClassCommand(
        Guid Id,
        string Name,
        int Practice,
        int Theory,
        int PR,
        Guid TypeId,
        bool HasPlanner
        ) : IRequest<ErrorOr<ClassResult>>;
}