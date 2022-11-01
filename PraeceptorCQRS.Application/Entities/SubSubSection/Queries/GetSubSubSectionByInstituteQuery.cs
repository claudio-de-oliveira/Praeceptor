using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SubSubSection.Common;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Queries
{
    public record GetSubSubSectionByInstituteQuery(
        Guid InstituteId
        ) : IRequest<ErrorOr<SubSubSectionListResult>>;
}
