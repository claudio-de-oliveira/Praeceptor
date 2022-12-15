using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Component.Commands.UpdateCommand
{
    public class UpdateComponentCommandHandler
        : IRequestHandler<UpdateComponentCommand, ErrorOr<ComponentResult>>
    {
        private readonly IComponentRepository _repository;

        public UpdateComponentCommandHandler(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ComponentResult>> Handle(UpdateComponentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Component
            {
                CourseId = request.CourseId,
                Curriculum = request.Curriculum,
                Season = request.Season,
                ClassId = request.ClassId,
                AxisTypeId = request.AxisTypeId,
                Optative = request.Optative
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Component.Canceled;

            var created = await _repository.UpdateComponent(entity);

            if (created is null)
                return Domain.Errors.Error.Component.DataBaseError;

            return new ComponentResult(created);
        }
    }
}
