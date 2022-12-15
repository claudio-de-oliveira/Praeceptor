using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.DeleteCommand
{
    public class DeleteVariableXCommandHandler
        : IRequestHandler<DeleteVariableXCommand, ErrorOr<VariableResultX>>
    {
        private readonly IVariableXRepository _repository;

        public DeleteVariableXCommandHandler(IVariableXRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableResultX>> Handle(DeleteVariableXCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableX.Canceled;

            var deleted = await _repository.DeleteVariable(request.Id);
            if (deleted is null)
                return Domain.Errors.Error.VariableX.NotFound;

            return new VariableResultX(deleted);
        }
    }
}
