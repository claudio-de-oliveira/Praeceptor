using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Commands;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("preceptorregimetype")]
    public class PreceptorRegimeTypeController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PreceptorRegimeTypeController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorRegimeTypeCountByInstitute(Guid instituteId)
        {
            var query = new GetPreceptorRegimeTypeByInstituteCountQuery(
                instituteId
                );

            ErrorOr<PreceptorRegimeTypeCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorRegimeTypePage([FromBody] GetPreceptorRegimeTypePageRequest request)
        {
            var query = _mapper.Map<GetPreceptorRegimeTypePageQuery>(request);
            // var query = new GetPreceptorRegimeTypePageQuery(
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

            ErrorOr<PreceptorRegimeTypePageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<PreceptorRegimeTypeResponse>>(result.Page)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorRegimeTypeById(Guid id)
        {
            var query = new GetPreceptorRegimeTypeByIdQuery(
                id
                );

            ErrorOr<PreceptorRegimeTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PreceptorRegimeTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreceptorRegimeTypeByCode(Guid instituteId, string code)
        {
            var query = new GetPreceptorRegimeTypeByCodeQuery(
                code,
                instituteId
                );

            ErrorOr<PreceptorRegimeTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PreceptorRegimeTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdatePreceptorRegimeType([FromBody] UpdatePreceptorRegimeTypeRequest request)
        {
            var command = _mapper.Map<UpdatePreceptorRegimeTypeCommand>(request);

            ErrorOr<PreceptorRegimeTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreatePreceptorRegimeType([FromBody] CreatePreceptorRegimeTypeRequest request)
        {
            // var command = _mapper.Map<CreatePreceptorRegimeTypeCommand>(request);
            var command = new CreatePreceptorRegimeTypeCommand(
                request.Code,
                request.InstituteId
                );

            ErrorOr<PreceptorRegimeTypeResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreatePreceptorRegimeType), _mapper.Map<PreceptorRegimeTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeletePreceptorRegimeType(Guid id)
        {
            var command = new DeletePreceptorRegimeTypeCommand(id);

            ErrorOr<PreceptorRegimeTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}

