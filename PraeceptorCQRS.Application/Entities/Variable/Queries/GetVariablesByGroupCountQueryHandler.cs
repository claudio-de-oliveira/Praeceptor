using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    internal class GetVariablesByGroupCountQueryHandler
        : IRequestHandler<GetVariablesByGroupCountQuery, ErrorOr<VariablesCountResult>>
    {
        private readonly IVariableRepository _repository;

        public GetVariablesByGroupCountQueryHandler(IVariableRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariablesCountResult>> Handle(GetVariablesByGroupCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Variable.Canceled;

            var count = await _repository.GetVariableCountByGroup(request.GroupId);
            if (count == -1)
                return Domain.Errors.Error.Variable.NotFound;

            return new VariablesCountResult(count);
        }
    }
}
