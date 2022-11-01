using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Component.Queries
{
    public class GetComponentByCourseAndCurriculumAndSeasonQueryHandler
        : IRequestHandler<GetComponentByCourseAndCurriculumAndSeasonQuery, ErrorOr<ComponentListResult>>
    {
        private readonly IComponentRepository _repository;

        public GetComponentByCourseAndCurriculumAndSeasonQueryHandler(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ComponentListResult>> Handle(GetComponentByCourseAndCurriculumAndSeasonQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Component.Canceled;

            var lst = await _repository.GetComponentListByCourseAndCurriculum(
                request.CourseId,
                request.Curriculum
                );

            var list = await _repository.GetComponentListByCourseAndCurriculumAndStage(
                request.CourseId, 
                request.Curriculum, 
                request.Season
                );

            if (list is null)
                return Domain.Errors.Error.Component.NotFound;

            return new ComponentListResult(list.ToList());
        }
    }
}
