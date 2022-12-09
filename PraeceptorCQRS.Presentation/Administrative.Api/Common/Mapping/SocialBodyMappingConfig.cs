using Mapster;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;
using PraeceptorCQRS.Application.Entities.SocialBody.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.SocialBody.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SocialBody;

namespace Administrative.Api.Common.Mapping;

public class SocialBodyMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetSocialBodyPageRequest, GetSocialBodyEntryPageQuery>();
        config.NewConfig<CreateSocialBodyEntryRequest, CreateSocialBodyEntryCommand>();
        config.NewConfig<SocialBodyEntryResult, SocialBodyEntryResponse>()
            .Map(dest => dest, src => src.Entry);
        config.NewConfig<SocialBodyEntryPageResult, PageResponse<SocialBodyEntryResponse>>()
            .Map(dest => dest, src => src.Page);
        config.NewConfig<SocialBodyEntryListResult, List<SocialBodyEntryResponse>>()
            .Map(dest => dest, src => src.List);
    }
}
