using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public class GetSqlDocxStreamByIdQueryHandler
        : IRequestHandler<GetSqlDocxStreamByIdQuery, ErrorOr<DocxResult>>
    {
        private readonly IDocxStreamRepository _repository;

        public GetSqlDocxStreamByIdQueryHandler(IDocxStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<DocxResult>> Handle(GetSqlDocxStreamByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.ReadDocx(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Docx.NotFound;

            return new DocxResult(entity);
        }
    }
}