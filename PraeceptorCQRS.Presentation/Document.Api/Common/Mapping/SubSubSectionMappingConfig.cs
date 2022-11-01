using Mapster;

using PraeceptorCQRS.Application.Entities.SubSubSection.Commands;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SubSubSection;

namespace PraeceptorCQRS.Presentation.Document.Api.Common.Mapping
{
    public class SubSubSectionMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateSubSubSectionRequest, CreateSubSubSectionCommand>();
            config.NewConfig<UpdateSubSubSectionRequest, UpdateSubSubSectionCommand>();
            config.NewConfig<SubSubSectionResult, SubSubSectionResponse>()
                .Map(dest => dest, src => src.SubSubSection);
            config.NewConfig<SubSubSectionPageResult, PageResponse<SubSubSectionResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

