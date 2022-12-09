using ErrorOr;
using MediatR;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Queries
{
    public class GetSocialBodyEntriesCountByCourseQueryHandler
        : IRequestHandler<GetSocialBodyEntriesCountByCourseQuery, ErrorOr<SocialBodyEntriesCount>>
    {
        private readonly ISocialBodyRepository _repository;

        public GetSocialBodyEntriesCountByCourseQueryHandler(ISocialBodyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SocialBodyEntriesCount>> Handle(GetSocialBodyEntriesCountByCourseQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SocialBody.Canceled;

            var count = await _repository.GetEntriesCountByCourse(
                request.CourseId
                );

            return new SocialBodyEntriesCount(count);
        }
    }
}
