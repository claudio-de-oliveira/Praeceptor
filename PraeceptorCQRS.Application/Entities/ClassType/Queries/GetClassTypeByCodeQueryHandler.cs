using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ClassType.Queries
{
    public class GetClassTypeByCodeQueryHandler
        : IRequestHandler<GetClassTypeByCodeQuery, ErrorOr<ClassTypeResult>>
    {
        private readonly IClassTypeRepository _repository;

        public GetClassTypeByCodeQueryHandler(IClassTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassTypeResult>> Handle(GetClassTypeByCodeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetClassTypeByCode(request.InstituteId, request.Code);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.ClassType.Canceled;

            if (entity is null)
                return Domain.Errors.Error.ClassType.NotFound;

            return new ClassTypeResult(entity);
        }
    }
}
