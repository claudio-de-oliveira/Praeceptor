using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Document.Queries
{
    public class GetDocumentPageQueryHandler
        : IRequestHandler<GetDocumentPageQuery, ErrorOr<DocumentPageResult>>
    {
        private readonly IDocumentRepository _repository;

        public GetDocumentPageQueryHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<DocumentPageResult>> Handle(GetDocumentPageQuery request, CancellationToken cancellationToken)
        {
            var page = await _repository.GetDocumentPage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.TitleFilter,
                request.AuthorFilter,
                request.CreatedFilter,
                request.CreatedByFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Document.Canceled;

            if (page is null)
                return Domain.Errors.Error.Document.NotFound;

            return new DocumentPageResult(page);
        }
    }
}
