using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SubSubSection.Common;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Commands
{
    public record DeleteSubSubSectionCommand(
        Guid Id
        ) : IRequest<ErrorOr<SubSubSectionResult>>;
}

