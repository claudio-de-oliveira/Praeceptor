using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Document.Queries
{
    public class GetDocumentByIdQueryHandler
        : IRequestHandler<GetDocumentByIdQuery, ErrorOr<DocumentResult>>
    {
        private readonly IDocumentRepository _repository;

        public GetDocumentByIdQueryHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<DocumentResult>> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Document.Canceled;

            var entity = await _repository.GetDocumentById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Document.NotFound;

            return new DocumentResult(entity);
        }
    }
}

