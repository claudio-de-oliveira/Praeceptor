using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Institute.Common;

namespace PraeceptorCQRS.Application.Entities.Institute.Queries
{
    public record GetInstituteByCodeQuery(
        string Code
        ) : IRequest<ErrorOr<InstituteResult>>;
}
