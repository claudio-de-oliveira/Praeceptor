using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Section.Common;

namespace PraeceptorCQRS.Application.Entities.Section.Commands
{
    public record DeleteSectionCommand(
        Guid Id
        ) : IRequest<ErrorOr<SectionResult>>;
}

