using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Section.Common;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public record GetSectionByInstituteCountQuery(
        Guid InstituteId
        ) : IRequest<ErrorOr<SectionCountResult>>;
}
