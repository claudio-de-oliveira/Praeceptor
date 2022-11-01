using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.FileStream.Queries
{
    public class ExistSqlFileStreamCodeQueryHandler
        : IRequestHandler<ExistSqlFileStreamCodeQuery, ErrorOr<ExistFileStreamResult>>
    {
        private readonly IFileStreamRepository _repository;

        public ExistSqlFileStreamCodeQueryHandler(IFileStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ExistFileStreamResult>> Handle(ExistSqlFileStreamCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SqlFileStream.Canceled;

            bool exist = await Task.Run(() => _repository.Exists(request.InstituteId, request.Code));

            return new ExistFileStreamResult(exist);
        }
    }
}
