using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SubSection.Common;

namespace PraeceptorCQRS.Application.Entities.SubSection.Commands
{
    public record DeleteSubSectionCommand(
        Guid Id
        ) : IRequest<ErrorOr<SubSectionResult>>;
}

