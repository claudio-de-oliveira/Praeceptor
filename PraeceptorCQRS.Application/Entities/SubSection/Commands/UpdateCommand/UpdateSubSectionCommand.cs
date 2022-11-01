using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSection.Common;

namespace PraeceptorCQRS.Application.Entities.SubSection.Commands
{
    public record UpdateSubSectionCommand(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
        ) : IRequest<ErrorOr<SubSectionResult>>;
}

