using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

using Serilog;

namespace PraeceptorCQRS.Application.Entities.FileStream.Commands
{
    public class CreateFileCommandHandler
        : IRequestHandler<CreateFileCommand, ErrorOr<FileResult>>
    {
        private readonly IFileStreamRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateFileCommandHandler(IFileStreamRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<FileResult>> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.SqlFileStream(Guid.NewGuid())
            {
                Name = request.Name,
                Title = request.Title,
                Source = request.Source,
                Description = request.Description,
                Data = request.Data,
                InstituteId = request.InstituteId,
                ContentType = request.ContentType,
                DateCreated = _dateTimeProvider.UtcNow
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SqlFileStream.Canceled;

            var created = await _repository.StoreFile(entity);
            if (created is null)
            {
                Log.Error("Erro ao tentar salvar o documento");
                return Domain.Errors.Error.SqlFileStream.DataBaseError;
            }

            return new FileResult(created);
        }
    }
}