using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Commands
{
    public class DeletePreceptorCommandHandler
        : IRequestHandler<DeletePreceptorCommand, ErrorOr<PreceptorResult>>
    {
        private readonly IPreceptorRepository _repository;

        public DeletePreceptorCommandHandler(IPreceptorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorResult>> Handle(DeletePreceptorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetPreceptorById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Preceptor.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Preceptor.Canceled;

            await _repository.DeletePreceptor(request.Id);

            return new PreceptorResult(entity);
        }
    }
}

