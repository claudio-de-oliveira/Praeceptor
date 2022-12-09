using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Entities.SimpleTable.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Commands.UpdateCommand
{
    public class UpdateSimpleTableCommandHandler
        : IRequestHandler<UpdateSimpleTableCommand, ErrorOr<SimpleTableResult>>
    {
        private readonly ISimpleTableRepository tableRepository;

        public UpdateSimpleTableCommandHandler(ISimpleTableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task<ErrorOr<SimpleTableResult>> Handle(UpdateSimpleTableCommand request, CancellationToken cancellationToken)
        {
            var entity = await tableRepository.GetTableById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.SimpleTable.NotFound;

            var update = new Domain.Entities.SimpleTable(entity.Id)
            {
                Code = entity.Code,
                Title = request.Title,
                Header = request.Header,
                Rows = request.Rows,
                Footer = request.Footer,
                InstituteId = entity.InstituteId,
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = DateTime.UtcNow,
                LastModifiedBy = ""
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SimpleTable.Canceled;

            await tableRepository.UpdateTable(update);

            return new SimpleTableResult(update);
        }
    }
}