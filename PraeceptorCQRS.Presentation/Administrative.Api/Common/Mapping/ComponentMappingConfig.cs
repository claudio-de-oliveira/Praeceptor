using Mapster;

using PraeceptorCQRS.Application.Entities.Component.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.Component.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Contracts.Entities.Component;

namespace Administrative.Api.Common.Mapping
{
    public class ComponentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateComponentRequest, CreateComponentCommand>();
            config.NewConfig<UpdateComponentRequest, UpdateComponentCommand>();
            config.NewConfig<CurriculumResult, CurriculumResponse>()
                .Map(dest => dest, src => src.Curriculum);
            config.NewConfig<ComponentResult, ComponentResponse>()
                .Map(dest => dest, src => src.Component);
        }
    }
}
