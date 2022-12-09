using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.Document.Commands
{
    public record CreateDocumentCommand(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
        ) : IRequest<ErrorOr<DocumentResult>>;
}