using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.FileStream.Common;

namespace PraeceptorCQRS.Application.Entities.FileStream.Queries
{
    public record GetSqlFileStreamByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<SqlFileStreamResult>>;
}

