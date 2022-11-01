using Mapster;

using PraeceptorCQRS.Application.Entities.Group.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.Group.Common;
using PraeceptorCQRS.Contracts.Entities.Group;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace Document.Api.Common.Mapping
{
    public class GroupMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateGroupRequest, CreateGroupCommand>();
            config.NewConfig<GroupResult, GroupResponse>()
                .Map(dest => dest, src => src.Group);
            config.NewConfig<GroupPageResult, PageResponse<GroupResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}
