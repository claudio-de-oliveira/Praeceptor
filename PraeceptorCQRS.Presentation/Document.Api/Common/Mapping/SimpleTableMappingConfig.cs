using Mapster;

using PraeceptorCQRS.Application.Entities.SimpleTable.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.SimpleTable.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.SimpleTable.Common;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SimpleTable;

namespace Document.Api.Common.Mapping
{
    public class SimpleTableMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateSimpleTableRequest, CreateSimpleTableCommand>();
            config.NewConfig<UpdateSimpleTableRequest, UpdateSimpleTableCommand>();
            config.NewConfig<SimpleTableResult, SimpleTableResponse>()
                .Map(dest => dest, src => src.Table);
            config.NewConfig<SimpleTablePageResult, PageResponse<SimpleTableResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}