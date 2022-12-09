using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Class.Commands;
using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Application.Entities.Class.Queries;
using PraeceptorCQRS.Contracts.Entities.Class;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("class")]
    public class ClassController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ClassController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetClassCountByInstitute(Guid instituteId)
        {
            var query = new GetClassByInstituteCountQuery(
                instituteId
                );

            ErrorOr<ClassCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetClassPage([FromBody] GetClassPageRequest request)
        {
            // var query = _mapper.Map<GetClassPageQuery>(request);
            var query = new GetClassPageQuery(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.Code,
                request.Name,
                request.Practice,
                request.Theory,
                request.PR,
                request.TypeId,
                request.CreatedFilter,
                request.CreatedByFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            ErrorOr<ClassPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<ClassResponse>>(result/*.Page*/)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            var query = new GetClassByIdQuery(
                id
                );

            ErrorOr<ClassResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<ClassResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetClassByCode(string code)
        {
            var query = new GetClassByCodeQuery(
                code
                );

            ErrorOr<ClassResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<ClassResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateClass([FromBody] UpdateClassRequest request)
        {
            var command = _mapper.Map<UpdateClassCommand>(request);

            ErrorOr<ClassResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassRequest request)
        {
            var command = _mapper.Map<CreateClassCommand>(request);

            ErrorOr<ClassResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateClass), _mapper.Map<ClassResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteClass(Guid id)
        {
            var command = new DeleteClassCommand(id);

            ErrorOr<ClassResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}