using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Course.Queries
{
    public class GetCoursePageQueryHandler
        : IRequestHandler<GetCoursePageQuery, ErrorOr<CoursePageResult>>
    {
        private readonly ICourseRepository _repository;

        public GetCoursePageQueryHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<CoursePageResult>> Handle(GetCoursePageQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetCoursePage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.CodeFilter,
                request.NameFilter,
                request.ACFilter,
                request.SeasonsFilter,
                request.MinimumWorkloadFilter,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Course.Canceled;

            if (list is null)
                return Domain.Errors.Error.Course.NotFound;

            return new CoursePageResult(list);
        }
    }
}
