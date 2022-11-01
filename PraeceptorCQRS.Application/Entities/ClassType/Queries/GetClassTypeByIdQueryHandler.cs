using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ClassType.Queries
{
    public class GetClassTypeByIdQueryHandler
        : IRequestHandler<GetClassTypeByIdQuery, ErrorOr<ClassTypeResult>>
    {
        private readonly IClassTypeRepository _repository;

        public GetClassTypeByIdQueryHandler(IClassTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassTypeResult>> Handle(GetClassTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.ClassType.Canceled;

            var entity = await _repository.GetClassTypeById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.ClassType.NotFound;

            return new ClassTypeResult(entity);
        }
    }
}

