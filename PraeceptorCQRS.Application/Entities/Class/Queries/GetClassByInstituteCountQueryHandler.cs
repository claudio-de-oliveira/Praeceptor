using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Class.Queries
{
    public class GetClassByInstituteCountQueryHandler
        : IRequestHandler<GetClassByInstituteCountQuery, ErrorOr<ClassCountResult>>
    {
        private readonly IClassRepository _repository;

        public GetClassByInstituteCountQueryHandler(IClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassCountResult>> Handle(GetClassByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Class.Canceled;

            var count = await _repository.GetClassCountByInstitute(request.InstituteId);

            if (count == -1)
                return Domain.Errors.Error.Class.NotFound;

            return new ClassCountResult(count);
        }
    }
}
