using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ClassType.Common;

namespace PraeceptorCQRS.Application.Entities.ClassType.Queries
{
    public record GetClassTypeByInstituteCountQuery(
        Guid InstituteId
        ) : IRequest<ErrorOr<ClassTypeCountResult>>;
}
