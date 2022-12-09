using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Queries
{
    public class GetSocialBodyEntryQueryHandler
        : IRequestHandler<GetSocialBodyEntryQuery, ErrorOr<SocialBodyEntryResult>>
    {
        private readonly ISocialBodyRepository _repository;

        public GetSocialBodyEntryQueryHandler(ISocialBodyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SocialBodyEntryResult>> Handle(GetSocialBodyEntryQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SocialBody.Canceled;

            var item = await _repository.GetEntry(
                request.CourseId, request.PreceptorId, request.RoleId
                );

            if (item is null)
                return Domain.Errors.Error.SocialBody.NotFound;

            return new SocialBodyEntryResult(item);
        }
    }
}
