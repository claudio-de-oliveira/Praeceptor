using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Queries
{
    public record GetPreceptorPageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? Code,
        string? Name,
        string? Email,
        Guid? DegreeTypeId,
        Guid? RegimeTypeId,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        ) : IRequest<ErrorOr<PreceptorPageResult>>;
}
