using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Course.Commands
{
    public class CreateCourseCommandHandler
        : IRequestHandler<CreateCourseCommand, ErrorOr<CourseResult>>
    {
        private readonly ICourseRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateCourseCommandHandler(ICourseRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<CourseResult>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.Course.Create(
                request.Code,
                request.Name,
                request.AC,
                request.NumberOfSeasons,
                request.MinimumWorkload,
                request.Telephone,
                request.Email,
                request.Image,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                string.Empty
            );

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Course.Canceled;

            var created = await _repository.CreateCourse(entity);
            if (created is null)
                return Domain.Errors.Error.Course.DataBaseError;

            return new CourseResult(created);
        }
    }
}

