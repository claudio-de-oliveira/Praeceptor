using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Commands
{
    public record UpdatePreceptorDegreeTypeCommand(
        Guid Id,
        bool LatoSensu,
        bool StrictoSensu
        ) : IRequest<ErrorOr<PreceptorDegreeTypeResult>>;
}