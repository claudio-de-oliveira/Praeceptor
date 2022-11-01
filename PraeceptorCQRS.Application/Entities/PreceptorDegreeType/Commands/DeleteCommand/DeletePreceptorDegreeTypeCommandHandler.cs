using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Commands
{
    public class DeletePreceptorDegreeTypeCommandHandler
        : IRequestHandler<DeletePreceptorDegreeTypeCommand, ErrorOr<PreceptorDegreeTypeResult>>
    {
        private readonly IPreceptorDegreeTypeRepository _repository;

        public DeletePreceptorDegreeTypeCommandHandler(IPreceptorDegreeTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorDegreeTypeResult>> Handle(DeletePreceptorDegreeTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetPreceptorDegreeTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.PreceptorDegreeType.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorDegreeType.Canceled;

            await _repository.DeletePreceptorDegreeType(request.Id);

            return new PreceptorDegreeTypeResult(entity);
        }
    }
}

