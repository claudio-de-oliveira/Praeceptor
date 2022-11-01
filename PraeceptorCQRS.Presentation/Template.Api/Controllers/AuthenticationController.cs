// using PraeceptorCQRS.Contracts.Authentication;
// using PraeceptorCQRS.Domain.Errors;
// 
// using PraeceptorCQRS.Application.Authentication.Commands;
// using PraeceptorCQRS.Application.Authentication.Common;
// using PraeceptorCQRS.Application.Authentication.Queries;
// 
// using ErrorOr;
// using MediatR;
// 
// using Microsoft.AspNetCore.Mvc;
// using MapsterMapper;
// 
// namespace PraeceptorCQRS.Presentation.Template.Api.Controllers
// {
//     [Route("auth")]
//     public class AuthenticationController : ApiController
//     {
//         private readonly ISender _mediator;
//         private readonly IMapper _mapper;
// 
//         public AuthenticationController(IMediator mediator, IMapper mapper)
//         {
//             this._mediator = mediator;
//             this._mapper = mapper;
//         }
// 
//         [HttpPost("register")]
//         public async Task<IActionResult> Register(RegisterRequest request)
//         {
//             var command = _mapper.Map<RegisterCommand>(request);
// 
//             ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
// 
//             return authResult.Match(
//                 authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
//                 errors => Problem(errors)
//                 );
//         }
// 
//         [HttpPost("login")]
//         public async Task<IActionResult> Login(LoginRequest request)
//         {
//             var query = _mapper.Map<LoginQuery>(request);
//             ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);
// 
//             if (authResult.IsError && authResult.FirstError == Authentication.InvalidCredentials)
//             {
//                 return Problem(
//                     statusCode: StatusCodes.Status401Unauthorized,
//                     title: authResult.FirstError.Description);
//             }
// 
//             return authResult.Match(
//                 authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
//                 errors => Problem(errors)
//                 );
//         }
//     }
// }

