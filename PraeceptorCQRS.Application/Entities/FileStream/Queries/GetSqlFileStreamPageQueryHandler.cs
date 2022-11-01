using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.FileStream.Queries
{
    public class GetSqlFileStreamPageQueryHandler
        : IRequestHandler<GetSqlFileStreamPageQuery, ErrorOr<SqlFileStreamPageResult>>
    {
        private readonly IFileStreamRepository _repository;

        public GetSqlFileStreamPageQueryHandler(IFileStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SqlFileStreamPageResult>> Handle(GetSqlFileStreamPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SqlFileStream.Canceled;

            var list = await _repository.GetFilesPage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.NameFilter,
                request.TitleFilter,
                request.SourceByFilter,
                request.DescriptionFilter,
                request.ContentTypeFilter,
                request.DateCreatedFilter
                );

            if (list is null)
                return Domain.Errors.Error.SqlFileStream.NotFound;

            return new SqlFileStreamPageResult(list);
        }
    }
}
