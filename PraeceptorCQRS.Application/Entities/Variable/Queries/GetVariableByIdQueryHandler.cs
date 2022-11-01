using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public class GetVariableByIdQueryHandler
        : IRequestHandler<GetVariableByIdQuery, ErrorOr<VariableResult>>
    {
        private readonly IVariableRepository _repository;

        public GetVariableByIdQueryHandler(IVariableRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableResult>> Handle(GetVariableByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Variable.Canceled;

            var entity = await _repository.GetVariableById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Variable.NotFound;

            return new VariableResult(entity);
        }
    }
}
