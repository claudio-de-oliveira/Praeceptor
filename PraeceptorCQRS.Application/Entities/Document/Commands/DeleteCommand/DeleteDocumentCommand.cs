using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Document.Common;

namespace PraeceptorCQRS.Application.Entities.Document.Commands
{
    public record DeleteDocumentCommand(
        Guid Id
        ) : IRequest<ErrorOr<DocumentResult>>;
}

