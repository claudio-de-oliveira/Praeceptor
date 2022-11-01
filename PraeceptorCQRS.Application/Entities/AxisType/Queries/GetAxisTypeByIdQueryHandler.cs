using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.AxisType.Queries
{
    public class GetAxisTypeByIdQueryHandler
        : IRequestHandler<GetAxisTypeByIdQuery, ErrorOr<AxisTypeResult>>
    {
        private readonly IAxisTypeRepository _repository;
    
        public GetAxisTypeByIdQueryHandler(IAxisTypeRepository repository)
        {
            _repository = repository;
        }
    
        public async Task<ErrorOr<AxisTypeResult>> Handle(GetAxisTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.AxisType.Canceled;

            var entity = await _repository.GetAxisTypeById(request.Id);
    
            if (entity is null)
                return Domain.Errors.Error.AxisType.NotFound;
    
            return new AxisTypeResult(entity);
        }
    }
}
