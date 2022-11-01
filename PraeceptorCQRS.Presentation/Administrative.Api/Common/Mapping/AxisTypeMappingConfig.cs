using Mapster;

using PraeceptorCQRS.Application.Entities.AxisType.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.AxisType.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Application.Entities.AxisType.Queries;
using PraeceptorCQRS.Contracts.Entities.AxisType;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace Administrative.Api.Common.Mapping
{
    public class AxisTypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetAxisTypePageQuery, GetAxisTypePageRequest>();

            config.NewConfig<CreateAxisTypeRequest, CreateAxisTypeCommand>();
            config.NewConfig<UpdateAxisTypeRequest, UpdateAxisTypeCommand>();
            config.NewConfig<AxisTypeResult, AxisTypeResponse>()
                .Map(dest => dest, src => src.AxisType);
            config.NewConfig<AxisTypePageResult, PageResponse<AxisTypeResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}
