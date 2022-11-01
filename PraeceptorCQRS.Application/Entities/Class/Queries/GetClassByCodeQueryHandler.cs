using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Class.Queries
{
    public class GetClassByCodeQueryHandler
        : IRequestHandler<GetClassByCodeQuery, ErrorOr<ClassResult>>
    {
        private readonly IClassRepository _repository;

        public GetClassByCodeQueryHandler(IClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassResult>> Handle(GetClassByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Class.Canceled;

            var entity = await _repository.GetClassByCode(request.Code);

            if (entity is null)
                return Domain.Errors.Error.Class.NotFound;

            return new ClassResult(entity);
        }
    }
}
