using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public class GetSqlDocxStreamPageQueryHandler
        : IRequestHandler<GetDocxPageQuery, ErrorOr<DocxPageResult>>
    {
        private readonly IDocxStreamRepository _repository;

        public GetSqlDocxStreamPageQueryHandler(IDocxStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<DocxPageResult>> Handle(GetDocxPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Docx.Canceled;

            var list = await _repository.GetDocxPage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.TitleFilter,
                request.DescriptionFilter,
                request.ContentTypeFilter,
                request.DateCreatedFilter,
                request.CreatedByFilter
                );

            if (list is null)
                return Domain.Errors.Error.Docx.NotFound;

            return new DocxPageResult(list);
        }
    }
}