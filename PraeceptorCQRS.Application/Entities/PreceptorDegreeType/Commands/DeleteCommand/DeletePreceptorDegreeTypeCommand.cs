using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Commands
{
    public record DeletePreceptorDegreeTypeCommand(
        Guid Id
        ) : IRequest<ErrorOr<PreceptorDegreeTypeResult>>;
}

