using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Group.Queries
{
    public class GetGroupsByInstituteCountQueryHandler
        : IRequestHandler<GetGroupsByInstituteCountQuery, ErrorOr<GroupsCountResult>>
    {
        private readonly IGroupRepository _repository;

        public GetGroupsByInstituteCountQueryHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupsCountResult>> Handle(GetGroupsByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Group.Canceled;

            var count = await _repository.GetGroupCountByInstitute(request.InstituteId);
            if (count == -1)
                return Domain.Errors.Error.Group.NotFound;

            return new GroupsCountResult(count);
        }
    }
}
