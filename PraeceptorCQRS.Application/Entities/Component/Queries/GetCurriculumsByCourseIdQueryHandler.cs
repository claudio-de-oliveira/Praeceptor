using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Component.Queries;

public class GetCurriculumsByCourseIdQueryHandler
    : IRequestHandler<GetCurriculumsByCourseIdQuery, ErrorOr<CurriculumListResult>>
{
    private readonly IComponentRepository _repository;

    public GetCurriculumsByCourseIdQueryHandler(IComponentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<CurriculumListResult>> Handle(GetCurriculumsByCourseIdQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Domain.Errors.Error.Component.Canceled;

        var list = await _repository.GetCurriculumsByCourseId(request.Id);

        return new CurriculumListResult(list);
    }
}
