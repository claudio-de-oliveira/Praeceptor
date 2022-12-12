using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Preceptor.Commands;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Application.Entities.Preceptor.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.Preceptor;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("preceptor")]
    public class PreceptorController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PreceptorController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePreceptor([FromBody] CreatePreceptorRequest request)
        {
            var command = _mapper.Map<CreatePreceptorCommand>(request);
            // var command = new CreatePreceptorCommand(
            //     request.Code,
            //     request.Name,
            //     request.Email,
            //     request.Image,
            //     request.DegreeTypeId,
            //     request.RegimeTypeId,
            //     request.InstituteId
            //     );

            ErrorOr<PreceptorResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreatePreceptor), _mapper.Map<PreceptorResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorCountByInstitute(Guid instituteId)
        {
            var query = new GetPreceptorByInstituteCountQuery(
                instituteId
                );

            ErrorOr<PreceptorCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorPage([FromBody] GetPreceptorPageRequest request)
        {
            var query = _mapper.Map<GetPreceptorPageQuery>(request);
            // var query = new GetPreceptorPageQuery(
            //     request.InstituteId,
            //     request.Start,
            //     request.Count,
            //     request.Sort,
            //     request.Ascending,
            //     request.CodeFilter,
            //     request.NameFilter,
            //     request.EmailFilter,
            //     request.DegreeTypeFilter,
            //     request.RegimeTypeFilter,
            //     request.CreatedByFilter,
            //     request.CreatedFilter,
            //     request.LastModifiedFilter,
            //     request.LastModifiedByFilter
            //     );

            ErrorOr<PreceptorPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<PreceptorResponse>>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPreceptorById(Guid id)
        {
            var query = new GetPreceptorByIdQuery(
                id
                );

            ErrorOr<PreceptorResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PreceptorResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{code}")]
        //[Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPreceptorByCode(string code)
        {
            var query = new GetPreceptorByCodeQuery(
                code
                );

            ErrorOr<PreceptorResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PreceptorResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdatePreceptor([FromBody] UpdatePreceptorRequest request)
        {
            var command = _mapper.Map<UpdatePreceptorCommand>(request);

            ErrorOr<PreceptorResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeletePreceptor(Guid id)
        {
            var command = new DeletePreceptorCommand(id);

            ErrorOr<PreceptorResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}