using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Component.Queries;

public class GetCurriculaByCourseIdQueryHandler
    : IRequestHandler<GetCurriculaByCourseIdQuery, ErrorOr<CurriculumListResult>>
{
    private readonly IComponentRepository _repository;

    public GetCurriculaByCourseIdQueryHandler(IComponentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<CurriculumListResult>> Handle(GetCurriculaByCourseIdQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Domain.Errors.Error.Component.Canceled;

        var list = await _repository.GetCurriculaByCourseId(request.Id);

        return new CurriculumListResult(list);
    }
}
