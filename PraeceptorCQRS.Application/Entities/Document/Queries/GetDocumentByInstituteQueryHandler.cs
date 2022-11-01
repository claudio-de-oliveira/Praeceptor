using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Document.Queries
{
    public class GetDocumentByInstituteQueryHandler
        : IRequestHandler<GetDocumentByInstituteQuery, ErrorOr<DocumentListResult>>
    {
        private readonly IDocumentRepository _repository;

        public GetDocumentByInstituteQueryHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<DocumentListResult>> Handle(GetDocumentByInstituteQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetDocumentByInstitute(request.InstituteId);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Document.Canceled;

            if (list is null)
                return Domain.Errors.Error.Document.NotFound;

            return new DocumentListResult(list);
        }
    }
}
