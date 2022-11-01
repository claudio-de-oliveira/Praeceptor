using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SubSection.Common;

namespace PraeceptorCQRS.Application.Entities.SubSection.Queries
{
    public record GetSubSectionByInstitutePageQuery(
        Guid InstituteId,
        int PageStart,
        int PageSize
        ) : IRequest<ErrorOr<SubSectionListResult>>;
}
