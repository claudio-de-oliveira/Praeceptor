using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Commands.CreateCommand
{
    public class CreateSimpleTableCommandHandler
        : IRequestHandler<CreateSimpleTableCommand, ErrorOr<SimpleTableResult>>
    {
        private readonly IInstituteRepository instituteRepository;
        private readonly ISimpleTableRepository tableRepository;

        public CreateSimpleTableCommandHandler(ISimpleTableRepository tableRepository, IInstituteRepository instituteRepository)
        {
            this.tableRepository = tableRepository;
            this.instituteRepository = instituteRepository;
        }

        public async Task<ErrorOr<SimpleTableResult>> Handle(CreateSimpleTableCommand request, CancellationToken cancellationToken)
        {
            var institute = await instituteRepository
                .GetInstituteById(request.InstituteId);
            if (institute is null)
                return Domain.Errors.Error.Institute.NotFound;

            var entity = Domain.Entities.SimpleTable.Create(
                request.Code,
                request.Title,
                request.Header,
                request.Rows,
                request.Footer,
                request.InstituteId,
                string.Empty);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SimpleTable.Canceled;

            var created = await tableRepository.CreateTable(entity);
            if (created is null)
                return Domain.Errors.Error.SimpleTable.DataBaseError;

            return new SimpleTableResult(created);
        }
    }
}