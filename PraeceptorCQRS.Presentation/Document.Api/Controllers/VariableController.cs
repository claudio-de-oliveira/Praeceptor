using MapsterMapper;
using ErrorOr;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.Variable.Commands.DeleteCommand;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Entities.Variable.Queries;
using PraeceptorCQRS.Contracts.Entities.Variable;
using PraeceptorCQRS.Presentation.Document.Api.Controllers;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace Document.Api.Controllers
{
    [Route("variable")]
    public class VariableController : ApiController
    {
        protected readonly ISender _mediator;
        protected readonly IMapper _mapper;

        public VariableController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("exists/{groupId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> Exists(Guid groupId, string code)
        {
            var request = new ExistsVariableWithCodeQuery(groupId, code);

            ErrorOr<VariableExistResult> result = await _mediator.Send(request);

            return result.Match(
                result => Ok(result.Exist),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateVariable([FromBody] CreateVariableRequest request)
        {
            var command = _mapper.Map<CreateVariableCommand>(request);

            ErrorOr<VariableResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateVariable), _mapper.Map<VariableResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetVariableById(Guid id)
        {
            var query = new GetVariableByIdQuery(id);

            ErrorOr<VariableResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<VariableResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetVariableByCode(Guid instituteId, string code)
        {
            var query = new GetVariableByCodeQuery(instituteId, code);

            ErrorOr<VariableResult> result = await _mediator.Send(query);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => Ok(_mapper.Map<VariableResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/count/{groupId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetVariableCountByGroup(Guid groupId)
        {
            var query = new GetVariablesByGroupCountQuery(
                groupId
                );

            ErrorOr<VariablesCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetVariablePage([FromBody] GetVariablePageRequest request)
        {
            var query = _mapper.Map<GetVariablesByGroupPageQuery>(request);

            ErrorOr<VariablePageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<VariableResponse>>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/from/group/{groupId}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteVariablesFromGroup(Guid groupId)
        {
            var command = new DeleteVariablesFromGroupCommand(groupId);

            ErrorOr<bool> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteVariable(Guid id)
        {
            var command = new DeleteVariableCommand(id);

            ErrorOr<VariableResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}
