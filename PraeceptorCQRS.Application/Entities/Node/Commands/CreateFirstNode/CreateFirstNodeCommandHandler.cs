using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public class CreateFirstNodeCommandHandler
        : IRequestHandler<CreateFirstNodeCommand, ErrorOr<NodeResult>>
    {
        private readonly IListRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateFirstNodeCommandHandler(IListRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<NodeResult>> Handle(CreateFirstNodeCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.Node.Create(
                request.FirstEntityId,
                request.DocumentId,
                request.SecondEntityId,
                _dateTimeProvider.UtcNow,
                string.Empty);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            var created = await _repository.CreateFirst(entity);
            if (created is null)
                return Domain.Errors.Error.Node.DataBaseError;

            return new NodeResult(created);
        }
    }
}
