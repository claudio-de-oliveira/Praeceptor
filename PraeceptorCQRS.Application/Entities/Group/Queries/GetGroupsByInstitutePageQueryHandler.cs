using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Group.Queries
{
    public class GetGroupsByInstitutePageQueryHandler
        : IRequestHandler<GetGroupsByInstitutePageQuery, ErrorOr<GroupPageResult>>
    {
        private readonly IGroupRepository _repository;

        public GetGroupsByInstitutePageQueryHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupPageResult>> Handle(GetGroupsByInstitutePageQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetGroupPage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.Code
                );

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Group.Canceled;

            if (list is null)
                return Domain.Errors.Error.Group.NotFound;

            return new GroupPageResult(list);
        }
    }
}
