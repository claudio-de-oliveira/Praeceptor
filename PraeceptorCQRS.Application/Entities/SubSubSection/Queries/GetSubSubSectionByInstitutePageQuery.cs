using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SubSubSection.Common;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Queries
{
    public record GetSubSubSectionByInstitutePageQuery(
        Guid InstituteId,
        int PageStart,
        int PageSize
        ) : IRequest<ErrorOr<SubSubSectionListResult>>;
}
