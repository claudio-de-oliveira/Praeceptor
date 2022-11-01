using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ClassType.Common;

namespace PraeceptorCQRS.Application.Entities.ClassType.Commands
{
    public record UpdateClassTypeCommand(
        Guid Id
        ) : IRequest<ErrorOr<ClassTypeResult>>;
}

