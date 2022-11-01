using Mapster;

using PraeceptorCQRS.Application.Entities.User.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.User.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.User.Common;
using PraeceptorCQRS.Contracts.Entities.User;

namespace UserManager.Api.Common.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateUserRequest, CreateUserCommand>();
            config.NewConfig<UpdateUserRequest, UpdateUserCommand>();
            config.NewConfig<UserResult, UserResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
