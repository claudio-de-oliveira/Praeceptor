using Mapster;

using PraeceptorCQRS.Application.Entities.VariableValue.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.VariableValue.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;
using PraeceptorCQRS.Contracts.Entities.VariableValue;

namespace Document.Api.Common.Mapping
{
    public class VariableValueMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateVariableValueRequest, CreateVariableValueCommand>();
            config.NewConfig<UpdateVariableValueRequest, UpdateVariableValueCommand>();
            config.NewConfig<VariableValueResult, VariableValueResponse>()
                .Map(dest => dest, src => src.VariableValue);
        }
    }
}
