// using PraeceptorCQRS.Application.Authentication.Commands;
// using PraeceptorCQRS.Application.Authentication.Common;
// using PraeceptorCQRS.Application.Authentication.Queries;
// using PraeceptorCQRS.Contracts.Authentication;
// 
// using Mapster;
// 
// namespace PraeceptorCQRS.Presentation.Template.Api.Common.Mapping
// {
//     public class AuthenticationMappingConfig : IRegister
//     {
//         public void Register(TypeAdapterConfig config)
//         {
//             config.NewConfig<RegisterRequest, RegisterCommand>();
//             config.NewConfig<LoginRequest, LoginQuery>();
//             config.NewConfig<AuthenticationResult, AuthenticationResponse>()
//                 .Map(dest => dest, src => src.User);
//         }
//     }
// }

