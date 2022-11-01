using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.Class.Commands
{
    public record CreateClassCommand(
        string Code,
        string Name,
        int Practice,
        int Theory,
        int PR,
        Guid InstituteId,
        Guid TypeId
        ) : IRequest<ErrorOr<ClassResult>>;
}

