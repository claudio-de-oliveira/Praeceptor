using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Section.Common;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public record GetSectionByInstitutePageQuery(
        Guid InstituteId,
        int PageStart,
        int PageSize
        ) : IRequest<ErrorOr<SectionListResult>>;
}
