using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Section.Common;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public record GetSectionsInChapterListQuery(
        Guid ChapterId
        ) : IRequest<ErrorOr<SectionListResult>>;
}
