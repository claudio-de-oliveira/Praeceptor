using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Section.Common;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public record GetSectionByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<SectionResult>>;
}

