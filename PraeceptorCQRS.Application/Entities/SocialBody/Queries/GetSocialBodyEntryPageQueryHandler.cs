using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Queries
{
    public class GetSocialBodyEntryPageQueryHandler
        : IRequestHandler<GetSocialBodyEntryPageQuery, ErrorOr<SocialBodyEntryPageResult>>
    {
        private readonly ISocialBodyRepository _repository;

        public GetSocialBodyEntryPageQueryHandler(ISocialBodyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SocialBodyEntryPageResult>> Handle(GetSocialBodyEntryPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SocialBody.Canceled;

            var list = await _repository.GetEntryPage(
                request.CourseId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.CodeFilter,
                request.NameFilter,
                request.DegreeFilter,
                request.RegimeFilter,
                request.RoleFilter
                );

            if (list is null)
                return Domain.Errors.Error.SocialBody.NotFound;

            return new SocialBodyEntryPageResult(list);
        }
    }
}
