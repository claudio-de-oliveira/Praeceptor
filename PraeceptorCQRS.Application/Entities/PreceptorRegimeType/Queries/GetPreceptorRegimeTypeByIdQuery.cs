using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Queries
{
    public record GetPreceptorRegimeTypeByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<PreceptorRegimeTypeResult>>;
}

