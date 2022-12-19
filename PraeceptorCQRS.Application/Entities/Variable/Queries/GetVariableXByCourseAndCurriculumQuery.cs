using MediatR;
using ErrorOr;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public record GetVariableXByCourseAndCurriculumQuery(
        Guid Id,
        int Curriculum
        ) : IRequest<ErrorOr<VariableXListResult>>;
}
