using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Course.Commands
{
    public class DeleteCourseCommandHandler
        : IRequestHandler<DeleteCourseCommand, ErrorOr<CourseResult>>
    {
        private readonly ICourseRepository _repository;

        public DeleteCourseCommandHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<CourseResult>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetCourseById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Course.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Course.Canceled;

            await _repository.DeleteCourse(request.Id);

            return new CourseResult(entity);
        }
    }
}

