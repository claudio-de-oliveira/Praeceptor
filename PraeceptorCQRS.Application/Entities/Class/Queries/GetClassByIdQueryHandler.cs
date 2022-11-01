using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Class.Queries
{
    public class GetClassByIdQueryHandler
        : IRequestHandler<GetClassByIdQuery, ErrorOr<ClassResult>>
    {
        private readonly IClassRepository _repository;

        public GetClassByIdQueryHandler(IClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassResult>> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Class.Canceled;

            var entity = await _repository.GetClassById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Class.NotFound;

            return new ClassResult(entity);
        }
    }
}

