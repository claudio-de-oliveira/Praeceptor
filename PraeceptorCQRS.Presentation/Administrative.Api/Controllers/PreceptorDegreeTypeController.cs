using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Commands;
using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;
using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("preceptordegreetype")]
    public class PreceptorDegreeTypeController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PreceptorDegreeTypeController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorDegreeTypeCountByInstitute(Guid instituteId)
        {
            var query = new GetPreceptorDegreeTypeByInstituteCountQuery(
                instituteId
                );

            ErrorOr<PreceptorDegreeTypeCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorDegreeTypePage([FromBody] GetPreceptorDegreeTypePageRequest request)
        {
            var query = _mapper.Map<GetPreceptorDegreeTypePageQuery>(request);
            // var query = new GetPreceptorDegreeTypePageQuery(
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

            ErrorOr<PreceptorDegreeTypePageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<PreceptorDegreeTypeResponse>>(result/*.Page*/)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPreceptorDegreeTypeById(Guid id)
        {
            var query = new GetPreceptorDegreeTypeByIdQuery(
                id
                );

            ErrorOr<PreceptorDegreeTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PreceptorDegreeTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorDegreeTypeByCode(Guid instituteId, string code)
        {
            var query = new GetPreceptorDegreeTypeByCodeQuery(
                code,
                instituteId
                );

            ErrorOr<PreceptorDegreeTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PreceptorDegreeTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdatePreceptorDegreeType([FromBody] UpdatePreceptorDegreeTypeRequest request)
        {
            var command = _mapper.Map<UpdatePreceptorDegreeTypeCommand>(request);

            ErrorOr<PreceptorDegreeTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePreceptorDegreeType([FromBody] CreatePreceptorDegreeTypeRequest request)
        {
            // var command = _mapper.Map<CreatePreceptorDegreeTypeCommand>(request);
            var command = new CreatePreceptorDegreeTypeCommand(
                request.Code,
                request.LatoSensu,
                request.StrictoSensu,
                request.InstituteId
                );

            ErrorOr<PreceptorDegreeTypeResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreatePreceptorDegreeType), _mapper.Map<PreceptorDegreeTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeletePreceptorDegreeType(Guid id)
        {
            var command = new DeletePreceptorDegreeTypeCommand(id);

            ErrorOr<PreceptorDegreeTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}