using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public record GetSqlDocxStreamByInstituteCountQuery(
        Guid Id
        ) : IRequest<ErrorOr<SqlDocxStreamCountResult>>;
}