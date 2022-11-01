using Mapster;

using PraeceptorCQRS.Application.Entities.Preceptor.Commands;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Application.Entities.Preceptor.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.Preceptor;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Common.Mapping
{
    public class PreceptorMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetPreceptorPageRequest, GetPreceptorPageQuery>();

            config.NewConfig<CreatePreceptorRequest, CreatePreceptorCommand>();
            config.NewConfig<UpdatePreceptorRequest, UpdatePreceptorCommand>();
            config.NewConfig<PreceptorResult, PreceptorResponse>()
                .Map(dest => dest, src => src.Preceptor);
            config.NewConfig<PreceptorPageResult, PageResponse<PreceptorResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

