using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Document.Commands
{
    public class UpdateDocumentCommandHandler
        : IRequestHandler<UpdateDocumentCommand, ErrorOr<DocumentResult>>
    {
        private readonly IDocumentRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateDocumentCommandHandler(IDocumentRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<DocumentResult>> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetDocumentById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Document.NotFound;

            var updated = new Domain.Entities.Document(request.Id)
            {
                Title = request.Title,
                Text = request.Text,
                // Don't change InstituteId
                InstituteId = entity.InstituteId,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = request.UpdatedBy
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Document.Canceled;

            await _repository.UpdateDocument(updated);

            return new DocumentResult(updated);
        }
    }
}

