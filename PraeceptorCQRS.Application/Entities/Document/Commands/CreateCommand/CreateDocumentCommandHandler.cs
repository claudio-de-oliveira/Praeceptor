using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Document.Commands
{
    public class CreateDocumentCommandHandler
        : IRequestHandler<CreateDocumentCommand, ErrorOr<DocumentResult>>
    {
        private readonly IDocumentRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateDocumentCommandHandler(IDocumentRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<DocumentResult>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.Document.Create(
                request.Title,
                request.Text,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                request.CreatedBy);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Document.Canceled;

            var created = await _repository.CreateDocument(entity);
            if (created is null)
                return Domain.Errors.Error.Document.DataBaseError;

            return new DocumentResult(created);
        }
    }
}

