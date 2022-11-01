using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Commands
{
    public record CreatePreceptorDegreeTypeCommand(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorDegreeTypeResult>>;
}

