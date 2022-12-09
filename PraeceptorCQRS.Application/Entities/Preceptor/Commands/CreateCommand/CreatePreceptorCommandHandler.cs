using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Commands;

public class CreatePreceptorCommandHandler
    : IRequestHandler<CreatePreceptorCommand, ErrorOr<PreceptorResult>>
{
    private readonly IPreceptorRepository _repository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreatePreceptorCommandHandler(IPreceptorRepository repository, IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<PreceptorResult>> Handle(CreatePreceptorCommand request, CancellationToken cancellationToken)
    {
        var entity = Domain.Entities.Preceptor.Create(
            request.Code,
            request.Name,
            request.Email,
            request.Image,
            request.DegreeTypeId,
            request.RegimeTypeId,
            request.InstituteId,
            _dateTimeProvider.UtcNow,
            string.Empty
        );

        if (cancellationToken.IsCancellationRequested)
            return Domain.Errors.Error.Preceptor.Canceled;

        var created = await _repository.CreatePreceptor(entity);
        if (created is null)
            return Domain.Errors.Error.Preceptor.DataBaseError;

        return new PreceptorResult(created);
    }
}

