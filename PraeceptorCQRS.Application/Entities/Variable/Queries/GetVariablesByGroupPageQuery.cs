using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public record GetVariablesByGroupPageQuery(
        Guid GroupId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? Code
        ) : IRequest<ErrorOr<VariablePageResult>>;
}
