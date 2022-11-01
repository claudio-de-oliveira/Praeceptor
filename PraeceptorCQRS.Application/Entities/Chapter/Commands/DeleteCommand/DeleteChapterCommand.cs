using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Chapter.Common;

namespace PraeceptorCQRS.Application.Entities.Chapter.Commands
{
    public record DeleteChapterCommand(
        Guid Id
        ) : IRequest<ErrorOr<ChapterResult>>;
}

