using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public record GetDocxPageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? TitleFilter,
        string? DescriptionFilter,
        string? ContentTypeFilter,
        string? DateCreatedFilter,
        string? CreatedByFilter
        ) : IRequest<ErrorOr<DocxPageResult>>;
}