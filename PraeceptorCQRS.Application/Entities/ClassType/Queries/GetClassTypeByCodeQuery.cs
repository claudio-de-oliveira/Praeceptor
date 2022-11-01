using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.ClassType.Common;

namespace PraeceptorCQRS.Application.Entities.ClassType.Queries
{
    public record GetClassTypeByCodeQuery(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<ClassTypeResult>>;
}
