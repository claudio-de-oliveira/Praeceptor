using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;

namespace PraeceptorCQRS.Application.Entities.Section.Commands
{
    public record UpdateSectionCommand(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
        ) : IRequest<ErrorOr<SectionResult>>;
}

