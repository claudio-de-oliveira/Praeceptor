using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Course.Queries
{
    public class GetCourseByInstituteCountQueryHandler
        : IRequestHandler<GetCourseByInstituteCountQuery, ErrorOr<CourseCountResult>>
    {
        private readonly ICourseRepository _repository;

        public GetCourseByInstituteCountQueryHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<CourseCountResult>> Handle(GetCourseByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repository.GetCoursesCountByInstitute(request.InstituteId);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Course.Canceled;

            if (count == -1)
                return Domain.Errors.Error.Course.NotFound;

            return new CourseCountResult(count);
        }
    }
}
