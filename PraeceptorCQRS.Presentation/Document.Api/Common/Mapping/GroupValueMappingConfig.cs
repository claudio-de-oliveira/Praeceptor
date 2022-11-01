using Mapster;

using PraeceptorCQRS.Application.Entities.GroupValue.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.GroupValue.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;
using PraeceptorCQRS.Contracts.Entities.GroupValue;

namespace Document.Api.Common.Mapping
{
    public class GroupValueMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateGroupValueRequest, CreateGroupValueCommand>();
            config.NewConfig<UpdateGroupValueRequest, UpdateGroupValueCommand>();
            config.NewConfig<GroupValueResult, GroupValueResponse>()
                .Map(dest => dest, src => src.GroupValue);
        }
    }
}
