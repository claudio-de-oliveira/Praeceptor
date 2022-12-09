using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Class.Commands
{
    public class UpdateClassCommandHandler
        : IRequestHandler<UpdateClassCommand, ErrorOr<ClassResult>>
    {
        private readonly IClassRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateClassCommandHandler(IClassRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<ClassResult>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetClassById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Class.NotFound;

            var updated = new Domain.Entities.Class(request.Id)
            {
                Code = entity.Code,
                Name = request.Name,
                Practice = request.Practice,
                Theory = request.Theory,
                PR = request.PR,
                TypeId = request.TypeId,
                // don't change institute!
                InstituteId = entity.InstituteId,
                HasPlanner = request.HasPlanner,
                // ...
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = string.Empty
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Class.Canceled;

            await _repository.UpdateClass(updated);

            return new ClassResult(updated);
        }
    }
}