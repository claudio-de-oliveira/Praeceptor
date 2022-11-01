using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Queries
{
    public class GetVariableValueByVariableAndGroupValueQueryHandler
        : IRequestHandler<GetVariableValueByVariableAndGroupValueQuery, ErrorOr<VariableValueResult>>
    {
        private readonly IVariableValueRepository _repository;

        public GetVariableValueByVariableAndGroupValueQueryHandler(IVariableValueRepository repository)
        {
            this._repository = repository;
        }

        public async Task<ErrorOr<VariableValueResult>> Handle(GetVariableValueByVariableAndGroupValueQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            var entity = await _repository.GetVariableValueByVariableAndGroupValue(request.GroupValueId, request.VariableId);

            if (entity is null)
                return Domain.Errors.Error.VariableValue.NotFound;

            return new VariableValueResult(entity);
        }
    }
}
