using MapsterMapper;
using ErrorOr;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Group.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.Group.Commands.DeleteCommand;
using PraeceptorCQRS.Application.Entities.Group.Common;
using PraeceptorCQRS.Application.Entities.Group.Queries;
using PraeceptorCQRS.Contracts.Entities.Group;
using PraeceptorCQRS.Presentation.Document.Api.Controllers;
using Serilog;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace Document.Api.Controllers
{
    [Route("group")]
    public class GroupController : ApiController
    {
        protected readonly ISender _mediator;
        protected readonly IMapper _mapper;

        public GroupController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("exists/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> Exists(Guid instituteId, string code)
        {
            var request = new ExistsGroupWithCodeQuery(instituteId, code);

            ErrorOr<GroupExistResult> result = await _mediator.Send(request);

            return result.Match(
                result => Ok(result.Exist),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
        {
            var command = _mapper.Map<CreateGroupCommand>(request);

            ErrorOr<GroupResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateGroup), _mapper.Map<GroupResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var query = new GetGroupByIdQuery(id);

            ErrorOr<GroupResult> result = await _mediator.Send(query);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => Ok(_mapper.Map<GroupResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetGroupByCode(Guid instituteId, string code)
        {
            var query = new GetGroupByCodeQuery(instituteId, code);

            ErrorOr<GroupResult> result = await _mediator.Send(query);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => Ok(_mapper.Map<GroupResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetGroupsCountByInstitute(Guid instituteId)
        {
            var query = new GetGroupsByInstituteCountQuery(
                instituteId
                );

            ErrorOr<GroupsCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetGroupPage([FromBody] GetGroupPageRequest request)
        {
            var query = _mapper.Map<GetGroupsByInstitutePageQuery>(request);

            ErrorOr<GroupPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<GroupResponse>>(result.Page)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var command = new DeleteGroupCommand(id);

            ErrorOr<GroupResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}
