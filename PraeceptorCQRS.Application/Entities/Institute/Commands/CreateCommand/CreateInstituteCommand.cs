using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.Institute.Commands
{
    public record CreateInstituteCommand(
        string Acronym,
        string Name,
        string? Address,
        Guid HoldingId
        ) : IRequest<ErrorOr<InstituteResult>>;
}

