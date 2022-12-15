using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Queries
{
    public class GetCurriculumsByCourseQueryHandler
        : IRequestHandler<GetCurriculumsByCourseQuery, ErrorOr<CurriculumResultList>>
    {
        private readonly IComponentRepository _repository;

        public GetCurriculumsByCourseQueryHandler(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<CurriculumResultList>> Handle(GetCurriculumsByCourseQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Component.Canceled;

            var result = await _repository.GetCurriculumsByCourseId(request.CourseId);
            if (result is null)
                return Domain.Errors.Error.Course.NotFound;

            return new CurriculumResultList(result.Select(o => o.Curriculum).ToList());
        }
    }
}
