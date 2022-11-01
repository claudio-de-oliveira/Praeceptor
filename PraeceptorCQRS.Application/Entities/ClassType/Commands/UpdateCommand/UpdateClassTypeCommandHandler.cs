using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.ClassType.Commands
{
    public class UpdateClassTypeCommandHandler
        : IRequestHandler<UpdateClassTypeCommand, ErrorOr<ClassTypeResult>>
    {
        private readonly IClassTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateClassTypeCommandHandler(IClassTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<ClassTypeResult>> Handle(UpdateClassTypeCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.ClassType.Canceled;

            var entity = await _repository.GetClassTypeById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.ClassType.NotFound;

            var updated = new Domain.Entities.ClassType(request.Id)
            {
                Code = entity.Code,
                // don't change institute!
                InstituteId = entity.InstituteId,
                // ...
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = string.Empty
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.ClassType.Canceled;

            await _repository.UpdateClassType(updated);

            return new ClassTypeResult(updated);
        }
    }
}

