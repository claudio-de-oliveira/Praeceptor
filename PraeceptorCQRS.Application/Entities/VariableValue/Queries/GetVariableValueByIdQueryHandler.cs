using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Queries
{
    public class GetVariableValueByIdQueryHandler
        : IRequestHandler<GetVariableValueByIdQuery, ErrorOr<VariableValueResult>>
    {
        private readonly IVariableValueRepository _repository;

        public GetVariableValueByIdQueryHandler(IVariableValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableValueResult>> Handle(GetVariableValueByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            var entity = await _repository.GetVariableValueById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.VariableValue.NotFound;

            return new VariableValueResult(entity);
        }
    }
}
