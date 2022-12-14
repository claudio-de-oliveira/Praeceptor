using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Chapter.Common;

namespace PraeceptorCQRS.Application.Entities.Chapter.Queries
{
    public record GetChaptersInDocumentListQuery(
        Guid DocumentId
        ) : IRequest<ErrorOr<ChapterListResult>>;
}
