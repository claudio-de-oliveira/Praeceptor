using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public class ExistsVariableWithCodeQueryHandler
        : IRequestHandler<ExistsVariableWithCodeQuery, ErrorOr<VariableExistResult>>
    {
        private readonly IVariableRepository _repository;

        public ExistsVariableWithCodeQueryHandler(IVariableRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableExistResult>> Handle(ExistsVariableWithCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Variable.Canceled;

            var exist = await _repository.Exists(o => o.GroupId == request.GroupId && o.Code == request.Code);

            return new VariableExistResult(exist);
        }
    }
}