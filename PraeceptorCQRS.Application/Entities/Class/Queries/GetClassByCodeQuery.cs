using ErrorOr;
using MediatR;

using PraeceptorCQRS.Application.Entities.Class.Common;

namespace PraeceptorCQRS.Application.Entities.Class.Queries
{
    public record GetClassByCodeQuery(
        string Code
        ) : IRequest<ErrorOr<ClassResult>>;
}
