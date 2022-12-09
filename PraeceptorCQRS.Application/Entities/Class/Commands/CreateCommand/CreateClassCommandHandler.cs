using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Class.Commands
{
    public class CreateClassCommandHandler
        : IRequestHandler<CreateClassCommand, ErrorOr<ClassResult>>
    // , IRequestHandler<UpdateClassCommand, ErrorOr<ClassResult>>
    // , IRequestHandler<DeleteClassCommand, ErrorOr<ClassResult>>
    {
        private readonly IClassRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateClassCommandHandler(IClassRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<ClassResult>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.Class.Create(
                request.Code,
                request.Name,
                request.Practice,
                request.Theory,
                request.PR,
                request.TypeId,
                request.InstituteId,
                request.HasPlanner,
                _dateTimeProvider.UtcNow,
                string.Empty
                );

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Class.Canceled;

            var created = await _repository.CreateClass(entity);
            if (created is null)
                return Domain.Errors.Error.Class.DataBaseError;

            return new ClassResult(created);
        }

        // public async Task<ErrorOr<ClassResult>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        // {
        //     var entity = await _repository.GetClassById(request.Id);
        //
        //     if (entity is null)
        //         return Domain.Errors.Error.Class.NotFound;
        //
        //     var updated = new Domain.Entities.Class(request.Id)
        //     {
        //         Code = entity.Code,
        //         Name = request.Name,
        //         Practice = request.Practice,
        //         Theory = request.Theory,
        //         PR = request.PR,
        //         TypeId = request.TypeId,
        //         // don't change institute!
        //         InstituteId = entity.InstituteId,
        //         // ...
        //         Created = entity.Created,
        //         CreatedBy = entity.CreatedBy,
        //         LastModified = _dateTimeProvider.UtcNow,
        //         LastModifiedBy = string.Empty
        //     };
        //
        //     if (cancellationToken.IsCancellationRequested)
        //         return Domain.Errors.Error.Class.Canceled;
        //
        //     await _repository.UpdateClass(updated);
        //
        //     return new ClassResult(updated);
        // }
        //
        // public async Task<ErrorOr<ClassResult>> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        // {
        //     if (cancellationToken.IsCancellationRequested)
        //         return Domain.Errors.Error.Class.Canceled;
        //
        //     var entity = await _repository.GetClassById(request.Id);
        //     if (entity is null)
        //         return Domain.Errors.Error.Class.NotFound;
        //
        //     if (cancellationToken.IsCancellationRequested)
        //         return Domain.Errors.Error.Class.Canceled;
        //
        //     await _repository.DeleteClass(request.Id);
        //
        //     return new ClassResult(entity);
        // }
    }
}