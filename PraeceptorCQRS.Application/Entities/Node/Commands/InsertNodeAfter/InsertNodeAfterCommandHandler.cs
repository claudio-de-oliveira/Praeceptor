using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public class InsertNodeAfterCommandHandler
        : IRequestHandler<InsertNodeAfterCommand, ErrorOr<NodeResult>>
    {
        private readonly IListRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public InsertNodeAfterCommandHandler(IListRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<NodeResult>> Handle(InsertNodeAfterCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            var entity = Domain.Entities.Node.Create(
                request.FirstEntityId,
                request.DocumentId,
                request.SecondEntityId,
                _dateTimeProvider.UtcNow,
                string.Empty
            );

            var created = await _repository.InsertAfter(request.Id, entity);
            if (created is null)
                return Domain.Errors.Error.Node.DataBaseError;

            return new NodeResult(created);
        }
    }
}
