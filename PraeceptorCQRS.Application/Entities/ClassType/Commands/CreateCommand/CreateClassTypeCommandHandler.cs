using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.ClassType.Commands
{
    public class CreateClassTypeCommandHandler
        : IRequestHandler<CreateClassTypeCommand, ErrorOr<ClassTypeResult>>
    {
        private readonly IClassTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateClassTypeCommandHandler(IClassTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<ClassTypeResult>> Handle(CreateClassTypeCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.ClassType.Canceled;

            var entity = Domain.Entities.ClassType.Create(
                request.Code,
                request.InstituteId,
                request.IsRemote,
                request.DurationInMinutes,
                _dateTimeProvider.UtcNow,
                string.Empty);

            var created = await _repository.CreateClassType(entity);
            if (created is null)
                return Domain.Errors.Error.ClassType.DataBaseError;

            return new ClassTypeResult(created);
        }
    }
}