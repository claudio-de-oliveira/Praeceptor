using Mapster;

using PraeceptorCQRS.Application.Entities.SubSection.Commands;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SubSection;

namespace PraeceptorCQRS.Presentation.Document.Api.Common.Mapping
{
    public class SubSectionMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateSubSectionRequest, CreateSubSectionCommand>();
            config.NewConfig<UpdateSubSectionRequest, UpdateSubSectionCommand>();
            config.NewConfig<SubSectionResult, SubSectionResponse>()
                .Map(dest => dest, src => src.SubSection);
            config.NewConfig<SubSectionPageResult, PageResponse<SubSectionResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

