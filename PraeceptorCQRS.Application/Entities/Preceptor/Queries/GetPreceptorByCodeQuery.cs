using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Preceptor.Common;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Queries
{
    public record GetPreceptorByCodeQuery(
        string Code
        ) : IRequest<ErrorOr<PreceptorResult>>;
}
