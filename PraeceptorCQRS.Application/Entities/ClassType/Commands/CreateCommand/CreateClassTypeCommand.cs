using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ClassType.Common;

namespace PraeceptorCQRS.Application.Entities.ClassType.Commands
{
    public record CreateClassTypeCommand(
        string Code,
        string Code3,
        Guid InstituteId,
        bool IsRemote,
        int DurationInMinutes
        ) : IRequest<ErrorOr<ClassTypeResult>>;
}