using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Commands
{
    public record UpdateSubSubSectionCommand(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
        ) : IRequest<ErrorOr<SubSubSectionResult>>;
}

