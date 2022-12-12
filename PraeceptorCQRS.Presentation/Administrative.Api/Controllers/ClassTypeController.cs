using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.ClassType.Commands;
using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Application.Entities.ClassType.Queries;
using PraeceptorCQRS.Contracts.Entities.ClassType;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("classtype")]
    public class ClassTypeController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ClassTypeController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetTypeClassCountByInstitute(Guid instituteId)
        {
            var query = new GetClassTypeByInstituteCountQuery(
                instituteId
                );

            ErrorOr<ClassTypeCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetClassTypePage([FromBody] GetClassTypePageRequest request)
        {
            var query = _mapper.Map<GetClassTypePageQuery>(request);
            // var query = new GetClassTypePageQuery(
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

            ErrorOr<ClassTypePageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<ClassTypeResponse>>(result/*.Page*/)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetClassTypeById(Guid id)
        {
            var query = new GetClassTypeByIdQuery(
                id
                );

            ErrorOr<ClassTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<ClassTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetClassTypeByCode(Guid instituteId, string code)
        {
            var query = new GetClassTypeByCodeQuery(
                code,
                instituteId
                );

            ErrorOr<ClassTypeResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<ClassTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateClassType([FromBody] UpdateClassTypeRequest request)
        {
            var command = _mapper.Map<UpdateClassTypeCommand>(request);

            Console.WriteLine(request.Code3);

            ErrorOr<ClassTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateClassType([FromBody] CreateClassTypeRequest request)
        {
            var command = _mapper.Map<CreateClassTypeCommand>(request);
            // var command = new CreateClassTypeCommand(
            //     request.Code,
            //     request.Code3,
            //     request.InstituteId,
            //     request.IsRemote,
            //     request.DurationInMinutes
            //     );

            ErrorOr<ClassTypeResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateClassType), _mapper.Map<ClassTypeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteClassType(Guid id)
        {
            var command = new DeleteClassTypeCommand(id);

            ErrorOr<ClassTypeResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}