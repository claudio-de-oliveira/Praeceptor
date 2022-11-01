using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Document.Common;

namespace PraeceptorCQRS.Application.Entities.Document.Queries
{
    public record GetDocumentByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<DocumentResult>>;
}

