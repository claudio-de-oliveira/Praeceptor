using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Commands;
using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;
using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("preceptorroletype")]
    public class PreceptorRoleTypeController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PreceptorRoleTypeController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorRoleTypeCountByInstitute(Guid instituteId)
        {
            var query = new GetPreceptorRoleTypeByInstituteCountQuery(
                instituteId
                );

            ErrorOr<PreceptorRoleTypeCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorRoleTypePage([FromBody] GetPreceptorRoleTypePageRequest request)
        {
            var query = _mapper.Map<GetPreceptorRoleTypePageQuery>(request);
            // var query = new GetPreceptorRoleTypePageQuery(
            //     request.InstituteId,
            //     request.Start,
            //     request.Count,
            //     request.Sort,
            //     request.Ascending,
            //     request.CodeFilter,
            //     request.CreatedByFilter,
            //     request.CreatedFilter,
            //     request.LastModifiedFilter,
            //     request.LastModifiedByFilter
            //     );

            ErrorOr<PreceptorRoleTypePageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<PreceptorRoleTypeResponse>>(result/*.Page*/)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorRoleTypeById(Guid id)
        {
            var query = new GetPreceptorRoleTypeByIdQuery(
                id
                );

            ErrorOr<PreceptorRoleTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PreceptorRoleTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorRoleTypeByCode(Guid instituteId, string code)
        {
            var query = new GetPreceptorRoleTypeByCodeQuery(
                code,
                instituteId
                );

            ErrorOr<PreceptorRoleTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PreceptorRoleTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdatePreceptorRoleType([FromBody] UpdatePreceptorRoleTypeRequest request)
        {
            var command = _mapper.Map<UpdatePreceptorRoleTypeCommand>(request);

            ErrorOr<PreceptorRoleTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePreceptorRoleType([FromBody] CreatePreceptorRoleTypeRequest request)
        {
            var command = _mapper.Map<CreatePreceptorRoleTypeCommand>(request);
            // var command = new CreatePreceptorRoleTypeCommand(
            //     request.Code,
            //     request.Code3,
            //     request.InstituteId
            //     );

            ErrorOr<PreceptorRoleTypeResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreatePreceptorRoleType), _mapper.Map<PreceptorRoleTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeletePreceptorRoleType(Guid id)
        {
            var command = new DeletePreceptorRoleTypeCommand(id);

            ErrorOr<PreceptorRoleTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}