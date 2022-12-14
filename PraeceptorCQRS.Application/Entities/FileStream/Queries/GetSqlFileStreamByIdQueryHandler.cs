using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.FileStream.Queries
{
    public class GetSqlFileStreamByIdQueryHandler
        : IRequestHandler<GetSqlFileStreamByIdQuery, ErrorOr<FileResult>>
    {
        private readonly IFileStreamRepository _repository;

        public GetSqlFileStreamByIdQueryHandler(IFileStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<FileResult>> Handle(GetSqlFileStreamByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.ReadFile(request.Id);

            if (entity is null)
                return Domain.Errors.Error.SqlFileStream.NotFound;

            return new FileResult(entity);
        }
    }
}

