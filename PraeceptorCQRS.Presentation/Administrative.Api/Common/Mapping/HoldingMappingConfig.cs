using Mapster;

using PraeceptorCQRS.Application.Entities.Holding.Commands;
using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Entities.Holding.Queries;
using PraeceptorCQRS.Contracts.Entities.Holding;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Common.Mapping
{
    public class HoldingMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetHoldingPageRequest, GetHoldingPageQuery>();

            config.NewConfig<CreateHoldingRequest, CreateHoldingCommand>();
            config.NewConfig<UpdateHoldingRequest, UpdateHoldingCommand>();
            config.NewConfig<HoldingResult, HoldingResponse>()
                .Map(dest => dest, src => src.Holding);
            config.NewConfig<HoldingPageResult, PageResponse<HoldingResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}
