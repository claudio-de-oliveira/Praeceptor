using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries;

public record GetVariablesByCourseQuery(
    Guid CourseId
    ) : IRequest<ErrorOr<VariablesResult>>;