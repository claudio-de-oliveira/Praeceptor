using Mapster;

using PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.Variable;

namespace Document.Api.Common.Mapping
{
    public class VariableMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateVariableRequest, CreateVariableCommand>();
            config.NewConfig<VariableResult, VariableResponse>()
                .Map(dest => dest, src => src.Variable);
            config.NewConfig<VariablePageResult, PageResponse<VariableResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}
