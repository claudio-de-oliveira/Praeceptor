using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.FileStream.Queries
{
    public class GetSqlFileStreamByInstituteCountQueryHandler
        : IRequestHandler<GetSqlFileStreamByInstituteCountQuery, ErrorOr<SqlFileStreamCountResult>>
    {
        private readonly IFileStreamRepository _repository;

        public GetSqlFileStreamByInstituteCountQueryHandler(IFileStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SqlFileStreamCountResult>> Handle(GetSqlFileStreamByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repository.GetFileCountByInstitute(request.Id);

            if (count == -1)
                return Domain.Errors.Error.SqlFileStream.NotFound;

            return new SqlFileStreamCountResult(count);
        }
    }
}