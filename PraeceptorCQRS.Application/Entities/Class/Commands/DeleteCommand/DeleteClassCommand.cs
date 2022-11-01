using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Class.Common;

namespace PraeceptorCQRS.Application.Entities.Class.Commands
{
    public record DeleteClassCommand(
        Guid Id
        ) : IRequest<ErrorOr<ClassResult>>;
}

