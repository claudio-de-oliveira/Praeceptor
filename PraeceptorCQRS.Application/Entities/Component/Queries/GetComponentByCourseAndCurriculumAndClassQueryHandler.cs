using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Component.Queries
{
    public class GetComponentByCourseAndCurriculumAndClassQueryHandler
        : IRequestHandler<GetComponentByCourseAndCurriculumAndClassQuery, ErrorOr<ComponentResult>>
    {
        private readonly IComponentRepository _repository;

        public GetComponentByCourseAndCurriculumAndClassQueryHandler(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ComponentResult>> Handle(GetComponentByCourseAndCurriculumAndClassQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Component.Canceled;

            var entity = await _repository.GetComponentByCourseAndCurriculumAndClass(
                request.CourseId, 
                request.Curriculum,
                request.ClassId
                );

            if (entity is null)
                return Domain.Errors.Error.Component.NotFound;

            return new ComponentResult(entity);
        }
    }
}
