using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public record GetSectionPageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? TitleFilter,
        string? TextFilter,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        ) : IRequest<ErrorOr<SectionPageResult>>;
}
