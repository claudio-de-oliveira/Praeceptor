using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.ClassType.Commands
{
    public record CreateClassTypeCommand(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<ClassTypeResult>>;
}

