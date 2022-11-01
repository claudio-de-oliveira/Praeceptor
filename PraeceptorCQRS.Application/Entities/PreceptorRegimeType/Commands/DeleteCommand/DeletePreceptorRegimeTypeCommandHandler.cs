using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Commands
{
    public class DeletePreceptorRegimeTypeCommandHandler
        : IRequestHandler<DeletePreceptorRegimeTypeCommand, ErrorOr<PreceptorRegimeTypeResult>>
    {
        private readonly IPreceptorRegimeTypeRepository _repository;

        public DeletePreceptorRegimeTypeCommandHandler(IPreceptorRegimeTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorRegimeTypeResult>> Handle(DeletePreceptorRegimeTypeCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRegimeType.Canceled;

            var entity = await _repository.GetPreceptorRegimeTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.PreceptorRegimeType.NotFound;

            await _repository.DeletePreceptorRegimeType(request.Id);

            return new PreceptorRegimeTypeResult(entity);
        }
    }
}

