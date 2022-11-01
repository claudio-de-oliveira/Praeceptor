using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Component.Commands.DeleteCommand
{
    public class DeleteComponentCommandHandler
        : IRequestHandler<DeleteComponentCommand, ErrorOr<ComponentResult>>
    {
        private readonly IComponentRepository _repository;

        public DeleteComponentCommandHandler(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ComponentResult>> Handle(DeleteComponentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetComponentByCourseAndCurriculumAndClass(
                request.CourseId, 
                request.Curriculum, 
                request.ClassId
                );
            if (entity is null)
                return Domain.Errors.Error.Component.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Component.Canceled;

            await _repository.DeleteComponent(
                request.CourseId, 
                request.Curriculum,
                request.ClassId
                );

            return new ComponentResult(entity);
        }
    }
}

