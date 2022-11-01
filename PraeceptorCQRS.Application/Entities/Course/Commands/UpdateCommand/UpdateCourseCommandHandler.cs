using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Course.Commands
{
    public class UpdateCourseCommandHandler
        : IRequestHandler<UpdateCourseCommand, ErrorOr<CourseResult>>
    {
        private readonly ICourseRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateCourseCommandHandler(ICourseRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<CourseResult>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetCourseById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Course.NotFound;

            var updated = new Domain.Entities.Course(request.Id)
            {
                Code = entity.Code,
                Name = request.Name,
                CEO = request.CEO,
                AC = request.AC,
                NumberOfSeasons = request.NumberOfSeasons,
                MinimumWorkload = request.MinimumWorkload,
                Email = request.Email,
                Image = request.Image,
                Telephone = request.Telephone,
                // don't change institute!
                InstituteId = entity.InstituteId,
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = string.Empty
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Course.Canceled;

            await _repository.UpdateCourse(updated);

            return new CourseResult(updated);
        }
    }
}

