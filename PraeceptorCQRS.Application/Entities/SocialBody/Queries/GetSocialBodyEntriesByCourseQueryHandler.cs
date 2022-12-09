using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Queries
{
    public class GetSocialBodyEntriesByCourseQueryHandler
        : IRequestHandler<GetSocialBodyEntriesByCourseQuery, ErrorOr<SocialBodyEntryListResult>>
    {
        private readonly ISocialBodyRepository _repository;

        public GetSocialBodyEntriesByCourseQueryHandler(ISocialBodyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SocialBodyEntryListResult>> Handle(GetSocialBodyEntriesByCourseQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SocialBody.Canceled;
        
            var list = await _repository.GetEntriesByCourse(
                request.CourseId
                );
        
            if (list is null)
                return Domain.Errors.Error.SocialBody.NotFound;
        
            return new SocialBodyEntryListResult(list);
        }
    }
}
