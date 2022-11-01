using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.AxisType.Commands.DeleteCommand
{
    public class DeleteAxisTypeCommandHandler
        : IRequestHandler<DeleteAxisTypeCommand, ErrorOr<AxisTypeResult>>
    {
        private readonly IAxisTypeRepository _repository;
    
        public DeleteAxisTypeCommandHandler(IAxisTypeRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<ErrorOr<AxisTypeResult>> Handle(DeleteAxisTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAxisTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.AxisType.NotFound;
    
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.AxisType.Canceled;
    
            await _repository.DeleteAxisType(request.Id);
    
            return new AxisTypeResult(entity);
        }
    }
}

