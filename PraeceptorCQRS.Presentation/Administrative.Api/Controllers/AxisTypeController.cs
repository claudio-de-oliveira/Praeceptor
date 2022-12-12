using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.AxisType.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.AxisType.Commands.DeleteCommand;
using PraeceptorCQRS.Application.Entities.AxisType.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Application.Entities.AxisType.Queries;
using PraeceptorCQRS.Contracts.Entities.AxisType;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("axistype")]
    public class AxisTypeController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AxisTypeController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetAxisTypeCountByInstitute(Guid instituteId)
        {
            var query = new GetAxisTypeByInstituteCountQuery(
                instituteId
                );

            ErrorOr<AxisTypeCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetAxisTypePage([FromBody] GetAxisTypePageRequest request)
        {
            var query = _mapper.Map<GetAxisTypePageQuery>(request);

            ErrorOr<AxisTypePageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<AxisTypeResponse>>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetAxisTypeById(Guid id)
        {
            var query = new GetAxisTypeByIdQuery(
                id
                );

            ErrorOr<AxisTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<AxisTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetAxisTypeByCode(Guid instituteId, string code)
        {
            var query = new GetAxisTypeByCodeQuery(
                code,
                instituteId
                );

            ErrorOr<AxisTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<AxisTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateAxisType([FromBody] UpdateAxisTypeRequest request)
        {
            var command = _mapper.Map<UpdateAxisTypeCommand>(request);

            ErrorOr<AxisTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAxisType([FromBody] CreateAxisTypeRequest request)
        {
            var command = _mapper.Map<CreateAxisTypeCommand>(request);
            // var command = new CreateAxisTypeCommand(
            //     request.Code,
            //     request.Code3,
            //     request.InstituteId
            //     );

            ErrorOr<AxisTypeResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateAxisType), _mapper.Map<AxisTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteAxisType(Guid id)
        {
            var command = new DeleteAxisTypeCommand(id);

            ErrorOr<AxisTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}