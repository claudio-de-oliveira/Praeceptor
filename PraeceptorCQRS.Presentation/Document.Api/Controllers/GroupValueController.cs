using ErrorOr;

using MapsterMapper;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.GroupValue.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.GroupValue.Commands.DeleteCommand;
using PraeceptorCQRS.Application.Entities.GroupValue.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;
using PraeceptorCQRS.Application.Entities.GroupValue.Queries;

using PraeceptorCQRS.Contracts.Entities.GroupValue;
using PraeceptorCQRS.Presentation.Document.Api.Controllers;

namespace Document.Api.Controllers
{
    [Route("groupvalue")]
    public class GroupValueController : ApiController
    {
        protected readonly ISender _mediator;
        protected readonly IMapper _mapper;

        public GroupValueController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateGroupValue([FromBody] CreateGroupValueRequest request)
        {
            var command = _mapper.Map<CreateGroupValueCommand>(request);

            ErrorOr<GroupValueResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateGroupValue), _mapper.Map<GroupValueResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetGroupValueById(Guid id)
        {
            var query = new GetGroupValueByIdQuery(id);

            ErrorOr<GroupValueResult> result = await _mediator.Send(query);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => Ok(_mapper.Map<GroupValueResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/group/{groupId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetGroupValuesByGroup(Guid groupId)
        {
            var query = new GetGroupValuesByGroupQuery(groupId);

            ErrorOr<GroupValueListResult> result = await _mediator.Send(query);

            var items = _mapper.Map<List<GroupValueResponse>>(result);

            return result.Match(
                result => Ok(_mapper.Map<List<GroupValueResponse>>(result.GroupValueList)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateGroupValue(UpdateGroupValueRequest request)
        {
            var command = _mapper.Map<UpdateGroupValueCommand>(request);

            ErrorOr<GroupValueResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => Ok(_mapper.Map<GroupValueResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/from/group/{groupId}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteGroupValuesFromGroup(Guid groupId)
        {
            var command = new DeleteGroupValuesFromGroupCommand(groupId);

            ErrorOr<bool> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteGroupValue(Guid id)
        {
            var command = new DeleteGroupValueCommand(id);

            ErrorOr<GroupValueResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}
