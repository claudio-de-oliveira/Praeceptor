using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;

namespace PraeceptorCQRS.Application.Entities.Document.Queries
{
    public record GetDocumentPageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? TitleFilter,
        string? AuthorFilter,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        ) : IRequest<ErrorOr<DocumentPageResult>>;
}
