using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.FileStream.Common;

namespace PraeceptorCQRS.Application.Entities.FileStream.Commands
{
    public record CreateSqlFileStreamCommand(
        string Name,
        string Title,
        string Source,
        string Description,
        byte[] Data,
        string ContentType,
        Guid InstituteId
        ) : IRequest<ErrorOr<SqlFileStreamResult>>;
}

