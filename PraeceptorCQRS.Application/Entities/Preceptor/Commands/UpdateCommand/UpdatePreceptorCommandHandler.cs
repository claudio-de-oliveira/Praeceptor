using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Commands
{
    public class UpdatePreceptorCommandHandler
        : IRequestHandler<UpdatePreceptorCommand, ErrorOr<PreceptorResult>>
    {
        private readonly IPreceptorRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdatePreceptorCommandHandler(IPreceptorRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<PreceptorResult>> Handle(UpdatePreceptorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetPreceptorById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Preceptor.NotFound;

            var updated = Domain.Entities.Preceptor.Create( //request.Id)
                entity.Code,
                request.Name,
                request.Email,
                request.Image,
                request.DegreeTypeId,
                request.RegimeTypeId,
                // don't change institute!
                entity.InstituteId,
                // don't change Created!
                entity.Created,
                // // don't change CreatedBy!
                entity.CreatedBy
                // _dateTimeProvider.UtcNow,
                // string.Empty
                );
            //};

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Preceptor.Canceled;

            await _repository.UpdatePreceptor(updated);

            return new PreceptorResult(updated);
        }
    }
}

