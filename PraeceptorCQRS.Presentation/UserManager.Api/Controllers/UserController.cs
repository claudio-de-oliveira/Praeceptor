using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.User.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.User.Commands.DeleteCommand;
using PraeceptorCQRS.Application.Entities.User.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.User.Common;
using PraeceptorCQRS.Application.Entities.User.Queries;
using PraeceptorCQRS.Contracts.Entities.User;

using Serilog;

namespace UserManager.Api.Controllers
{
    [Route("user")]
    public class UserController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public UserController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetUserCount()
        {
            var query = new GetUserCountQuery(
                );

            ErrorOr<UserCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetUserPage([FromBody] GetUserPageRequest request)
        {
            var query = new GetUserPageQuery(
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.HoldingIdFilter,
                request.InstituteIdFilter,
                request.CourseIdFilter,
                request.UserNameFilter,
                request.EmailFilter,
                request.PhoneNumberFilter,
                request.EnabledFilter,
                request.GenderFilter

                );

            ErrorOr<UserListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<UserResponse>>(result.Users)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery(
                id
                );

            ErrorOr<UserResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<UserResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            // var command = _mapper.Map<UpdateClassCommand>(request);
            var command = new UpdateUserCommand(
                request.Id,
                request.UserName,
                request.Email,
                request.PasswordHash,
                request.PhoneNumber,
                request.IsEnabled,
                request.Gender,
                request.HoldingId,
                request.InstituteId,
                request.CourseId
                );

            ErrorOr<UserResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            Log.Information($"Criando usuário {request.UserName}");

            var command = _mapper.Map<CreateUserCommand>(request);

            ErrorOr<UserResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateUser), _mapper.Map<UserResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand(id);

            ErrorOr<UserResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}
