using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Institute.Commands;
using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Entities.Institute.Queries;
using PraeceptorCQRS.Contracts.Entities.Institute;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("institute")]
    public class InstituteController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public InstituteController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{holdingId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetInstituteCountByInstitute(Guid holdingId)
        {
            var query = new GetInstituteByHoldingCountQuery(
                holdingId
                );

            ErrorOr<InstituteCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetInstitutePage([FromBody] GetInstitutePageRequest request)
        {
            var query = _mapper.Map<GetInstitutePageQuery>(request);

            ErrorOr<InstitutePageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<InstituteResponse>>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetInstituteById(Guid id)
        {
            var query = new GetInstituteByIdQuery(
                id
                );

            ErrorOr<InstituteResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<InstituteResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        //[Authorize("UpdatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateInstitute([FromBody] UpdateInstituteRequest request)
        {
            var command = _mapper.Map<UpdateInstituteCommand>(request);

            ErrorOr<InstituteResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(UpdateInstitute), _mapper.Map<InstituteResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateInstitute([FromBody] CreateInstituteRequest request)
        {
            // var command = _mapper.Map<CreateInstituteCommand>(request);
            var command = new CreateInstituteCommand(
                request.Acronym,
                request.Name,
                request.Address,
                request.HoldingId
                );

            ErrorOr<InstituteResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateInstitute), _mapper.Map<InstituteResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteInstitute(Guid id)
        {
            var command = new DeleteInstituteCommand(id);

            ErrorOr<InstituteResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}