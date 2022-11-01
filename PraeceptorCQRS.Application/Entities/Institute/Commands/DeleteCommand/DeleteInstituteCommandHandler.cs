using ErrorOr;
using MediatR;

using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Institute.Commands
{
    public class DeleteInstituteCommandHandler
        : IRequestHandler<DeleteInstituteCommand, ErrorOr<InstituteResult>>
    {
        private readonly IInstituteRepository _repository;

        public DeleteInstituteCommandHandler(IInstituteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<InstituteResult>> Handle(DeleteInstituteCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Institute.Canceled;

            var entity = await _repository.GetInstituteById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Institute.NotFound;

            entity.Delete();

            await _repository.DeleteInstitute(request.Id);

            return new InstituteResult(entity);
        }
    }
}

