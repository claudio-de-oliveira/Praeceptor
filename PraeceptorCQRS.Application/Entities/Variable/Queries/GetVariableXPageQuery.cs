using MediatR;
using ErrorOr;

using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public record GetVariableXPageQuery(
        Guid HoldingId,
        Guid InstituteId,
        Guid CourseId,
        int Curriculum,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? NameFilter,
        string? ValueFilter
        ) : IRequest<ErrorOr<VariableXPageResult>>;
}
