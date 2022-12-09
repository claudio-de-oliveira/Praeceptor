using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Pea.Command;
using PraeceptorCQRS.Application.Entities.Pea.Command.DeleteCommand;
using PraeceptorCQRS.Application.Entities.Pea.Command.UpdateCommand;
using PraeceptorCQRS.Application.Entities.Pea.Common;
using PraeceptorCQRS.Application.Entities.Pea.Queries;
using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.Pea;

namespace Pea.Api.Controllers
{
    [Route("Planner")]
    public class PeaController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PeaController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("id/{id:guid}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlannerFromId(Guid id)
        {
            var query = new GetPeaByIdQuery(id);

            ErrorOr<PeaResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Pea),
                errors => Problem(errors)
                );
        }

        // GET api/planner/id/{classId}
        [HttpGet("class/{classId:guid}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlannerFromClassId(Guid classId)
        {
            var query = new GetPeaPageQuery(
                classId,
                0,
                int.MaxValue,
                "Created",
                false,
                null,
                null,
                null,
                null
                );
            // var command = new GetPeaByIdQuery(classId);

            ErrorOr<PeaPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Page.Entities),
                errors => Problem(errors)
                );
        }

        [HttpPost("page")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlannerPage([FromBody] GetPeaPageRequest request)
        {
            // var query = _mapper.Map<GetClassPageQuery>(request);
            var query = new GetPeaPageQuery(
                request.ClassId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.CreatedFilter,
                request.CreatedByFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            ErrorOr<PeaPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<PeaResponse>>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/text/{peaId}/{season}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlannerTextById(Guid peaId, int season)
        {
            var query = new GetPeaTextByIdQuery(
                peaId,
                season
                );

            ErrorOr<PlannerTextResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PlannerTextResult>(result)),
                errors => Problem(errors)
                );
        }

        // POST api/planner
        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePlanner([FromBody] CreatePeaRequest request)
        {
            // var command = _mapper.Map<CreatePeaCommand>(request);
            var command = new CreatePeaCommand(
                request.ClassId,
                request.Text,
                request.CreatedBy
                );

            ErrorOr<PeaResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreatePlanner), _mapper.Map<PeaResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        // [Authorize("UpdatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdatePlanner([FromBody] UpdatePeaRequest request)
        {
            var command = _mapper.Map<UpdatePeaCommand>(request);

            ErrorOr<PeaResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{classId:guid}")]
        // [Authorize("DeletePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> DeletePlanner(Guid classId)
        {
            var command = new DeletePeaCommand(classId);

            ErrorOr<PeaResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}