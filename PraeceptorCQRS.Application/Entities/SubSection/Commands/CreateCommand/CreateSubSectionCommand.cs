using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.SubSection.Commands
{
    public record CreateSubSectionCommand(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
        ) : IRequest<ErrorOr<SubSectionResult>>;
}

