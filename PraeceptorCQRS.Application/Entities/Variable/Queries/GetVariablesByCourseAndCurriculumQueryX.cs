using MediatR;
using ErrorOr;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public record GetVariablesByCourseAndCurriculumQueryX(
        Guid Id,
        string? Curriculum
        ) : IRequest<ErrorOr<VariableXListResult>>;
}
