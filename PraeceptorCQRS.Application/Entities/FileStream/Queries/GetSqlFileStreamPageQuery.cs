using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.FileStream.Common;

namespace PraeceptorCQRS.Application.Entities.FileStream.Queries
{
    public record GetSqlFileStreamPageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? NameFilter,
        string? TitleFilter,
        string? SourceByFilter,
        string? DescriptionFilter,
        string? ContentTypeFilter,
        string? DateCreatedFilter
        ) : IRequest<ErrorOr<SqlFileStreamPageResult>>;
}
