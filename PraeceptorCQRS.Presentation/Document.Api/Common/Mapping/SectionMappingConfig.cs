using Mapster;

using PraeceptorCQRS.Application.Entities.Section.Commands;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.Section;

namespace PraeceptorCQRS.Presentation.Document.Api.Common.Mapping
{
    public class SectionMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateSectionRequest, CreateSectionCommand>();
            config.NewConfig<UpdateSectionRequest, UpdateSectionCommand>();
            config.NewConfig<SectionResult, SectionResponse>()
                .Map(dest => dest, src => src.Section);
            config.NewConfig<SectionPageResult, PageResponse<SectionResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

