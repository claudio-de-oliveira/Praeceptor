using Mapster;

using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.Variable.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Contracts.Entities.Document;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.Variable;

namespace Document.Api.Common.Mapping
{
    public class VariableXMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateVariableXRequest, CreateVariableXByHoldingCommand>();
            config.NewConfig<CreateVariableXRequest, CreateVariableXByInstituteCommand>();
            config.NewConfig<CreateVariableXRequest, CreateVariableXByCourseCommand>();
            config.NewConfig<UpdateVariableXRequest, UpdateVariableXByHoldingCommand>();
            config.NewConfig<UpdateVariableXRequest, UpdateVariableXByInstituteCommand>();
            config.NewConfig<UpdateVariableXRequest, UpdateVariableXByCourseCommand>();
            config.NewConfig<VariableXListResult, List<VariableXResponse>>()
                .Map(dest => dest, src => src.List);
            config.NewConfig<VariableResultX, VariableXResponse>()
                .Map(dest => dest, src => src.Variable);
            config.NewConfig<VariableXPageResult, PageResponse<VariableXResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}
