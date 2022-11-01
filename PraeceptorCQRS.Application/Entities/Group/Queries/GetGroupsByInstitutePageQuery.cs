using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;

namespace PraeceptorCQRS.Application.Entities.Group.Queries
{
    public record GetGroupsByInstitutePageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? Code
        ) : IRequest<ErrorOr<GroupPageResult>>;
}
