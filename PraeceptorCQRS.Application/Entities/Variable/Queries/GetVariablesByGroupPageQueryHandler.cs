using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    internal class GetVariablesByGroupPageQueryHandler
        : IRequestHandler<GetVariablesByGroupPageQuery, ErrorOr<VariablePageResult>>
    {
        private readonly IVariableRepository _repository;

        public GetVariablesByGroupPageQueryHandler(IVariableRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariablePageResult>> Handle(GetVariablesByGroupPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Variable.Canceled;

            var list = await _repository.GetVariablePage(
                request.GroupId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.Code
                );

            if (list is null)
                return Domain.Errors.Error.Variable.NotFound;

            return new VariablePageResult(list);
        }
    }
}
