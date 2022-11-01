using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ClassType.Queries
{
    public class GetClassTypeByInstituteCountQueryHandler
        : IRequestHandler<GetClassTypeByInstituteCountQuery, ErrorOr<ClassTypeCountResult>>
    {
        private readonly IClassTypeRepository _repository;

        public GetClassTypeByInstituteCountQueryHandler(IClassTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassTypeCountResult>> Handle(GetClassTypeByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repository.GetClassTypeCountByInstitute(request.InstituteId);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.ClassType.Canceled;

            if (count == -1)
                return Domain.Errors.Error.ClassType.NotFound;

            return new ClassTypeCountResult(count);
        }
    }
}
