using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Commands
{
    public class CreatePreceptorDegreeTypeCommandHandler
        : IRequestHandler<CreatePreceptorDegreeTypeCommand, ErrorOr<PreceptorDegreeTypeResult>>
    {
        private readonly IPreceptorDegreeTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreatePreceptorDegreeTypeCommandHandler(IPreceptorDegreeTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<PreceptorDegreeTypeResult>> Handle(CreatePreceptorDegreeTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.PreceptorDegreeType.Create(
                request.Code,
                request.LatoSensu,
                request.StrictoSensu,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                string.Empty
                );

            // var entity = new Domain.Entities.PreceptorDegreeType
            // {
            //     Code = request.Code,
            //     InstituteId = request.InstituteId,
            //     // ...
            //     Created = _dateTimeProvider.UtcNow,
            //     CreatedBy = string.Empty,
            //     LastModified = _dateTimeProvider.UtcNow,
            //     LastModifiedBy = string.Empty
            // };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorDegreeType.Canceled;

            var created = await _repository.CreatePreceptorDegreeType(entity);

            if (created is null)
                return Domain.Errors.Error.PreceptorDegreeType.DataBaseError;

            return new PreceptorDegreeTypeResult(created);
        }
    }
}