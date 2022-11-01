using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Holding.Commands;
using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Entities.Holding.Queries;
using PraeceptorCQRS.Contracts.Entities.Holding;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("holding")]
    public class HoldingController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public HoldingController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetHoldingCount()
        {
            var query = new GetHoldingByInstituteCountQuery(
                );

            ErrorOr<HoldingCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetHoldingPage([FromBody] GetHoldingPageRequest request)
        {
            var query = _mapper.Map<GetHoldingPageQuery>(request);
            // var query = new GetHoldingPageQuery(
            //     request.Start,
            //     request.Count,
            //     request.Sort,
            //     request.Ascending,
            //     request.AcronymFilter,
            //     request.NameFilter,
            //     request.AddressFilter,
            //     request.CreatedByFilter,
            //     request.CreatedFilter,
            //     request.LastModifiedFilter,
            //     request.LastModifiedByFilter
            //     );

            ErrorOr<HoldingPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<HoldingResponse>>(result.Page)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetHoldingById(Guid id)
        {
            var query = new GetHoldingByIdQuery(
                id
                );

            ErrorOr<HoldingResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<HoldingResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetHoldingByCode(string code)
        {
            var query = new GetHoldingByCodeQuery(
                code
                );

            ErrorOr<HoldingResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<HoldingResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateHolding([FromBody] UpdateHoldingRequest request)
        {
            var command = _mapper.Map<UpdateHoldingCommand>(request);

            ErrorOr<HoldingResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateHolding([FromBody] CreateHoldingRequest request)
        {
            var command = _mapper.Map<CreateHoldingCommand>(request);

            ErrorOr<HoldingResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateHolding), _mapper.Map<HoldingResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteHolding(Guid id)
        {
            var command = new DeleteHoldingCommand(id);

            ErrorOr<HoldingResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}

