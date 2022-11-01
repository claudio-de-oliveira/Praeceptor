using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;

namespace PraeceptorCQRS.Application.Entities.Document.Commands
{
    public record UpdateDocumentCommand(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
        ) : IRequest<ErrorOr<DocumentResult>>;
}

