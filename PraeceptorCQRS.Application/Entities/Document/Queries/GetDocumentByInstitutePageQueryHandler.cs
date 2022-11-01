using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Document.Queries
{
    public class GetDocumentByInstitutePageQueryHandler
        : IRequestHandler<GetDocumentByInstitutePageQuery, ErrorOr<DocumentListResult>>
    {
        private readonly IDocumentRepository _repository;

        public GetDocumentByInstitutePageQueryHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<DocumentListResult>> Handle(GetDocumentByInstitutePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Document.Canceled;

            var list = await _repository.GetDocumentPageByInstitute(request.InstituteId, request.PageStart, request.PageSize);
            if (list is null)
                return Domain.Errors.Error.Document.NotFound;

            return new DocumentListResult(list);
        }
    }
}
