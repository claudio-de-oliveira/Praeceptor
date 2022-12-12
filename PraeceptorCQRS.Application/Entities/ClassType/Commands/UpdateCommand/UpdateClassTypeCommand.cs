using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ClassType.Common;

namespace PraeceptorCQRS.Application.Entities.ClassType.Commands
{
    public record UpdateClassTypeCommand(
        Guid Id,
        string Code,
        string Code3
        ) : IRequest<ErrorOr<ClassTypeResult>>;
}

