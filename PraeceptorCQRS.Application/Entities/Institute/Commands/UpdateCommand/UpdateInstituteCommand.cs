using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Institute.Common;

namespace PraeceptorCQRS.Application.Entities.Institute.Commands
{
    public record UpdateInstituteCommand(
        Guid Id,
        string Name,
        string? Address
        ) : IRequest<ErrorOr<InstituteResult>>;
}

