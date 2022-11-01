using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public class CreateVariableCommandHandler
        : IRequestHandler<CreateVariableCommand, ErrorOr<VariableResult>>
    {
        private readonly IVariableRepository _repository;

        public CreateVariableCommandHandler(IVariableRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableResult>> Handle(CreateVariableCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Variable()
            {
                Code = request.Code,
                GroupId = request.GroupId
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Variable.Canceled;

            var created = await _repository.CreateVariable(entity);
            if (created is null)
                return Domain.Errors.Error.Variable.DataBaseError;

            return new VariableResult(created);
        }
    }
}
