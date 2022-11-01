using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Queries
{
    public record GetPreceptorDegreeTypeByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<PreceptorDegreeTypeResult>>;
}

