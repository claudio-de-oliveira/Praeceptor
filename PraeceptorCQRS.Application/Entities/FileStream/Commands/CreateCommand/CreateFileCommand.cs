using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.FileStream.Common;

namespace PraeceptorCQRS.Application.Entities.FileStream.Commands
{
    public record CreateFileCommand(
        string Name,
        string Title,
        string Source,
        string Description,
        byte[] Data,
        string ContentType,
        Guid InstituteId
        ) : IRequest<ErrorOr<FileResult>>;
}