using Mapster;

using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Commands;
using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;
using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Common.Mapping
{
    public class PreceptorDegreeTypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetPreceptorDegreeTypePageRequest, GetPreceptorDegreeTypePageQuery>();

            config.NewConfig<CreatePreceptorDegreeTypeRequest, CreatePreceptorDegreeTypeCommand>();
            config.NewConfig<UpdatePreceptorDegreeTypeRequest, UpdatePreceptorDegreeTypeCommand>();
            config.NewConfig<PreceptorDegreeTypeResult, PreceptorDegreeTypeResponse>()
                .Map(dest => dest, src => src.PreceptorDegreeType);
            config.NewConfig<PreceptorDegreeTypePageResult, PageResponse<PreceptorDegreeTypeResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

