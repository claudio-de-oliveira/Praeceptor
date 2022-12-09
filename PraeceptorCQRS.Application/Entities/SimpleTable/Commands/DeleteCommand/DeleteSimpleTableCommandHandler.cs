using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Commands.DeleteCommand
{
    public class DeleteSimpleTableCommandHandler
        : IRequestHandler<DeleteSimpleTableCommand, ErrorOr<SimpleTableResult>>
    {
        private readonly ISimpleTableRepository tableRepository;

        public DeleteSimpleTableCommandHandler(ISimpleTableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task<ErrorOr<SimpleTableResult>> Handle(DeleteSimpleTableCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SimpleTable.Canceled;

            var entity = await tableRepository.GetTableById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.SimpleTable.NotFound;

            await tableRepository.DeleteTable(request.Id);

            return new SimpleTableResult(entity);
        }
    }
}