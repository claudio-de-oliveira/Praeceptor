using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Queries
{
    public class GetCurriculaByCourseQueryHandler
        : IRequestHandler<GetCurriculaByCourseQuery, ErrorOr<CurriculumResultList>>
    {
        private readonly IComponentRepository _repository;

        public GetCurriculaByCourseQueryHandler(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<CurriculumResultList>> Handle(GetCurriculaByCourseQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Component.Canceled;

            var result = await _repository.GetCurriculaByCourseId(request.CourseId);
            if (result is null)
                return Domain.Errors.Error.Course.NotFound;

            return new CurriculumResultList(result.Select(o => o.Curriculum).ToList());
        }
    }
}
