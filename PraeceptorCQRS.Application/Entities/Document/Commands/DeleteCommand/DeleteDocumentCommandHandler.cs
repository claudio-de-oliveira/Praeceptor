using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Document.Commands
{
    public class DeleteDocumentCommandHandler
        : IRequestHandler<DeleteDocumentCommand, ErrorOr<DocumentResult>>
    {
        private readonly IDocumentRepository _repository;

        public DeleteDocumentCommandHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<DocumentResult>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetDocumentById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Document.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Document.Canceled;

            await _repository.DeleteDocument(request.Id);

            return new DocumentResult(entity);
        }
    }
}
