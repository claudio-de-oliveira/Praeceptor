using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Course.Queries
{
    public class GetCourseByIdQueryHandler
        : IRequestHandler<GetCourseByIdQuery, ErrorOr<CourseResult>>
    {
        private readonly ICourseRepository _repository;

        public GetCourseByIdQueryHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<CourseResult>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetCourseById(request.Id);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Course.Canceled;

            if (entity is null)
                return Domain.Errors.Error.Course.NotFound;

            return new CourseResult(entity);
        }
    }
}

