using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SubSection.Common;

namespace PraeceptorCQRS.Application.Entities.SubSection.Queries
{
    public record GetSubSectionByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<SubSectionResult>>;
}

