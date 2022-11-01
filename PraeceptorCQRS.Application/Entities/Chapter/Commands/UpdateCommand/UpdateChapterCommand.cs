using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;

namespace PraeceptorCQRS.Application.Entities.Chapter.Commands
{
    public record UpdateChapterCommand(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
        ) : IRequest<ErrorOr<ChapterResult>>;
}

