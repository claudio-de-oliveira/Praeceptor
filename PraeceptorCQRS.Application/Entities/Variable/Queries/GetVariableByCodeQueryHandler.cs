using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public class GetVariableByCodeQueryHandler
        : IRequestHandler<GetVariableByCodeQuery, ErrorOr<VariableResult>>
    {
        private readonly IVariableRepository _repository;

        public GetVariableByCodeQueryHandler(IVariableRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableResult>> Handle(GetVariableByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            var entity = await _repository.GetVariableByCode(request.GroupId, request.Code);
            if (entity is null)
                return Domain.Errors.Error.Variable.NotFound;

            return new VariableResult(entity);
        }
    }
}
