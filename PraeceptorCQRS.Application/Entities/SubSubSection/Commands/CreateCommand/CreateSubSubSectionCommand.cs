using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Commands
{
    public record CreateSubSubSectionCommand(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
        ) : IRequest<ErrorOr<SubSubSectionResult>>;
}

