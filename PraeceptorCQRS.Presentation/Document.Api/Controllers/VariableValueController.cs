using MapsterMapper;
using ErrorOr;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraeceptorCQRS.Presentation.Document.Api.Controllers;
using PraeceptorCQRS.Application.Entities.VariableValue.Commands.CreateCommand;
using PraeceptorCQRS.Contracts.Entities.VariableValue;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;
using PraeceptorCQRS.Application.Entities.VariableValue.Queries;
using PraeceptorCQRS.Application.Entities.VariableValue.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.VariableValue.Commands.DeleteCommand;

namespace Document.Api.Controllers
{
    [Route("variablevalue")]
    public class VariableValueController : ApiController
    {
        protected readonly ISender _mediator;
        protected readonly IMapper _mapper;

        public VariableValueController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateVariableValue([FromBody] CreateVariableValueRequest request)
        {
            var command = _mapper.Map<CreateVariableValueCommand>(request);

            ErrorOr<VariableValueResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateVariableValue), _mapper.Map<VariableValueResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetVariableValueById(Guid id)
        {
            var query = new GetVariableValueByIdQuery(id);

            ErrorOr<VariableValueResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<VariableValueResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/groupvalue/variable/{groupValueId}/{variableId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetVariableValueByVariableAndGroupValue(Guid groupValueId, Guid variableId)
        {
            var query = new GetVariableValueByVariableAndGroupValueQuery(groupValueId, variableId);

            ErrorOr<VariableValueResult> result = await _mediator.Send(query);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => Ok(_mapper.Map<VariableValueResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateVariableValue(UpdateVariableValueRequest request)
        {
            var command = _mapper.Map<UpdateVariableValueCommand>(request);

            ErrorOr<VariableValueResult> result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<VariableValueResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/from/variable/{variableId}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteVariableValuesFromVariable(Guid variableId)
        {
            var command = new DeleteVariableValuesFromVariableCommand(variableId);

            ErrorOr<bool> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteVariableValue(Guid id)
        {
            var command = new DeleteVariableValueCommand(id);

            ErrorOr<VariableValueResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}
