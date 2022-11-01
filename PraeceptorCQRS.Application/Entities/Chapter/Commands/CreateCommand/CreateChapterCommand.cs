using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.Chapter.Commands
{
    public record CreateChapterCommand(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
        ) : IRequest<ErrorOr<ChapterResult>>;
}

