using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Component.Queries
{
    public class GetComponentByCourseAndCurriculumQueryHandler
        : IRequestHandler<GetComponentByCourseAndCurriculumQuery, ErrorOr<ComponentListResult>>
    {
        private readonly IComponentRepository _repository;

        public GetComponentByCourseAndCurriculumQueryHandler(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ComponentListResult>> Handle(GetComponentByCourseAndCurriculumQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Component.Canceled;

            var list = await _repository.GetComponentListByCourseAndCurriculum(
                request.CourseId, 
                request.Curriculum
                );

            if (list is null)
                return Domain.Errors.Error.Component.NotFound;

            return new ComponentListResult(list.ToList());
        }
    }
}
