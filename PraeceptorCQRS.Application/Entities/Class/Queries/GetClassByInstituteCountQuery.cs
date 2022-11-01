using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Class.Common;

namespace PraeceptorCQRS.Application.Entities.Class.Queries
{
    public record GetClassByInstituteCountQuery(
        Guid InstituteId
        ) : IRequest<ErrorOr<ClassCountResult>>;
}
