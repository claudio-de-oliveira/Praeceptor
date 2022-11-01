using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Queries
{
    public record GetPreceptorRegimeTypeByCodeQuery(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorRegimeTypeResult>>;
}
