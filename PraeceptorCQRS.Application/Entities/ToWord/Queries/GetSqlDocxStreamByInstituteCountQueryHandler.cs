using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public class GetSqlDocxStreamByInstituteCountQueryHandler
        : IRequestHandler<GetSqlDocxStreamByInstituteCountQuery, ErrorOr<SqlDocxStreamCountResult>>
    {
        private readonly IDocxStreamRepository _repository;

        public GetSqlDocxStreamByInstituteCountQueryHandler(IDocxStreamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SqlDocxStreamCountResult>> Handle(GetSqlDocxStreamByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repository.GetDocxCountByInstitute(request.Id);

            if (count == -1)
                return Domain.Errors.Error.Docx.NotFound;

            return new SqlDocxStreamCountResult(count);
        }
    }
}