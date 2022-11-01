using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Document.Queries
{
    public class GetDocumentByInstituteCountQueryHandler
        : IRequestHandler<GetDocumentByInstituteCountQuery, ErrorOr<DocumentCountResult>>
    {
        private readonly IDocumentRepository _repository;

        public GetDocumentByInstituteCountQueryHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<DocumentCountResult>> Handle(GetDocumentByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Document.Canceled;

            var count = await _repository.GetDocumentsCountByInstitute(request.InstituteId);
            if (count == -1)
                return Domain.Errors.Error.Document.NotFound;

            return new DocumentCountResult(count);
        }
    }
}
