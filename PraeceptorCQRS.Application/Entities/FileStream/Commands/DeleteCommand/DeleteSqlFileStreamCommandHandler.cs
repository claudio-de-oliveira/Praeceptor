using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.FileStream.Commands
{
    public class DeleteSqlFileStreamCommandHandler
        : IRequestHandler<DeleteSqlFileStreamCommand, ErrorOr<FileResult>>
    {
        private readonly IFileStreamRepository _repository;

        public DeleteSqlFileStreamCommandHandler(IFileStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<FileResult>> Handle(DeleteSqlFileStreamCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetFileInfo(request.Id);
            if (entity is null)
                return Domain.Errors.Error.SqlFileStream.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SqlFileStream.Canceled;

            await _repository.DeleteFile(request.Id);

            return new FileResult(entity);
        }
    }
}
