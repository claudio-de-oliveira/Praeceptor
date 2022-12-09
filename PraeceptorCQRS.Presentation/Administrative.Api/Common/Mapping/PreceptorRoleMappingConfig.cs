using Mapster;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Commands;
using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;
using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Common.Mapping
{
    public class PreceptorRoleTypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetPreceptorRoleTypePageRequest, GetPreceptorRoleTypePageQuery>();

            config.NewConfig<CreatePreceptorRoleTypeRequest, CreatePreceptorRoleTypeCommand>();
            config.NewConfig<UpdatePreceptorRoleTypeRequest, UpdatePreceptorRoleTypeCommand>();
            config.NewConfig<PreceptorRoleTypeResult, PreceptorRoleTypeResponse>()
                .Map(dest => dest, src => src.PreceptorRoleType);
            config.NewConfig<PreceptorRoleTypePageResult, PageResponse<PreceptorRoleTypeResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}
