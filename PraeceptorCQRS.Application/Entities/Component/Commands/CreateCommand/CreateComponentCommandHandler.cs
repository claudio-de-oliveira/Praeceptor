using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Component.Commands.CreateCommand
{
    public class CreateComponentCommandHandler
        : IRequestHandler<CreateComponentCommand, ErrorOr<ComponentResult>>
    {
        private readonly IComponentRepository _repository;

        public CreateComponentCommandHandler(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ComponentResult>> Handle(CreateComponentCommand request, CancellationToken cancellationToken)
        {
            var previous = await _repository.GetComponentByCourseAndCurriculumAndClass(
                request.CourseId, 
                request.Curriculum, 
                request.ClassId
                );
            if (previous is not null)
                return Domain.Errors.Error.Component.DuplicateCode;

            var entity = new Domain.Entities.Component
            {
                CourseId = request.CourseId,
                Curriculum = request.Curriculum,
                Season = request.Season,
                ClassId = request.ClassId,
                Optative = request.Optative,
                AxisTypeId = request.AxisTypeId
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Component.Canceled;

            var created = await _repository.CreateComponent(entity);
            if (created is null)
                return Domain.Errors.Error.Component.DataBaseError;

            return new ComponentResult(created);
        }
    }
}
