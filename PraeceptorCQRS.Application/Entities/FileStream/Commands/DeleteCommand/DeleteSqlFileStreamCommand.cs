using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.FileStream.Common;

namespace PraeceptorCQRS.Application.Entities.FileStream.Commands
{
    public record DeleteSqlFileStreamCommand(
        Guid Id
        ) : IRequest<ErrorOr<SqlFileStreamResult>>;
}

