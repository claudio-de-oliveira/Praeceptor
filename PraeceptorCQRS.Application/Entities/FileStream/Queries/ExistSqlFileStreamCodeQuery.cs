using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.FileStream.Common;

namespace PraeceptorCQRS.Application.Entities.FileStream.Queries
{
    public record ExistSqlFileStreamCodeQuery(
        Guid InstituteId,
        string Code
        ) : IRequest<ErrorOr<ExistFileStreamResult>>;
}
