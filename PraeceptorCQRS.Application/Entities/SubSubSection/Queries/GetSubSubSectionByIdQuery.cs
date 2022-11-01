using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SubSubSection.Common;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Queries
{
    public record GetSubSubSectionByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<SubSubSectionResult>>;
}

