using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.FileStream.Queries
{
    public class GetSqlFileStreamByCodeQueryHandler
        : IRequestHandler<GetSqlFileStreamByCodeQuery, ErrorOr<SqlFileStreamResult>>
    {
        private readonly IFileStreamRepository _repository;

        public GetSqlFileStreamByCodeQueryHandler(IFileStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SqlFileStreamResult>> Handle(GetSqlFileStreamByCodeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.ReadFile(request.InstituteId, request.Code);

            if (entity is null)
                return Domain.Errors.Error.SqlFileStream.NotFound;

            return new SqlFileStreamResult(entity);
        }
    }
}
