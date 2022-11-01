using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.Section.Commands
{
    public record CreateSectionCommand(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
        ) : IRequest<ErrorOr<SectionResult>>;
}

