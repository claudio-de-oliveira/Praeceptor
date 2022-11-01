using Mapster;

using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Commands;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Common.Mapping
{
    public class PreceptorRegimeTypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetPreceptorRegimeTypePageRequest, GetPreceptorRegimeTypePageQuery>();

            config.NewConfig<CreatePreceptorRegimeTypeRequest, CreatePreceptorRegimeTypeCommand>();
            config.NewConfig<UpdatePreceptorRegimeTypeRequest, UpdatePreceptorRegimeTypeCommand>();
            config.NewConfig<PreceptorRegimeTypeResult, PreceptorRegimeTypeResponse>()
                .Map(dest => dest, src => src.PreceptorRegimeType);
            config.NewConfig<PreceptorRegimeTypePageResult, PageResponse<PreceptorRegimeTypeResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

