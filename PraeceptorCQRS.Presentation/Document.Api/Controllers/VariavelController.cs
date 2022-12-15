using MapsterMapper;
using ErrorOr;
using MediatR;

using PraeceptorCQRS.Presentation.Document.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Entities.Variable.Queries;
using PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand;
using PraeceptorCQRS.Contracts.Entities.Variable;
using PraeceptorCQRS.Application.Entities.Variable.Commands.DeleteCommand;
using PraeceptorCQRS.Application.Entities.Variable.Commands.UpdateCommand;

namespace Document.Api.Controllers
{
    [Route("variableX")]
    public class VariavelController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public VariavelController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create/holding")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateVariableByHolding([FromBody] CreateVariableXRequest request)
        {
            var command = _mapper.Map<CreateHoldingVariableXCommand>(request);

            ErrorOr<VariableResultX> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateVariableByHolding), _mapper.Map<VariableXResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("create/institute")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateVariableByInstitute([FromBody] CreateVariableXRequest request)
        {
            var command = _mapper.Map<CreateInstituteVariableXCommand>(request);

            ErrorOr<VariableResultX> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateVariableByInstitute), _mapper.Map<VariableXResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("create/course")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateVariableByCourse([FromBody] CreateVariableXRequest request)
        {
            var command = _mapper.Map<CreateCourseVariableXCommand>(request);

            ErrorOr<VariableResultX> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateVariableByCourse), _mapper.Map<VariableXResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update/holding")]
        // [Authorize("UpdatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateVariableByHolding([FromBody] UpdateVariableXRequest request)
        {
            var command = _mapper.Map<UpdateVariableXByHoldingCommand>(request);

            ErrorOr<VariableResultX> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPut("update/institute")]
        // [Authorize("UpdatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateVariableByInstitute([FromBody] UpdateVariableXRequest request)
        {
            var command = _mapper.Map<UpdateVariableXByInstituteCommand>(request);

            ErrorOr<VariableResultX> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPut("update/course")]
        // [Authorize("UpdatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateVariableByCourse([FromBody] UpdateVariableXRequest request)
        {
            var command = _mapper.Map<UpdateVariableXByCourseCommand>(request);

            ErrorOr<VariableResultX> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        // [Authorize("DeletePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteVariable(Guid id)
        {
            var command = new DeleteVariableXCommand(id);

            ErrorOr<VariableResultX> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/holding/{holdingId}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVariablesByHolding(Guid holdingId)
        {
            var query = new GetVariablesByHoldingQueryX(holdingId);

            ErrorOr<VariableXListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.List),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/institute/{instituteId}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVariablesByInstitute(Guid instituteId)
        {
            var query = new GetVariablesByInstituteQueryX(instituteId);

            ErrorOr<VariableXListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.List),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/course/{courseId:guid}/{curriculum:int}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVariablesByCourseAndCurriculum(Guid courseId, int curriculum)
        {
            var query = new GetVariablesByCourseAndCurriculumQueryX(courseId, curriculum.ToString());

            ErrorOr<VariableXListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.List),
                errors => Problem(errors)
                );
        }
    }
}
