using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Course.Commands;
using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Application.Entities.Course.Queries;
using PraeceptorCQRS.Contracts.Entities.Course;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Controllers
{
    [Route("course")]
    public class CourseController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public CourseController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetCourseCountByInstitute(Guid instituteId)
        {
            var query = new GetCourseByInstituteCountQuery(
                instituteId
                );

            ErrorOr<CourseCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetCoursePage([FromBody] GetCoursePageRequest request)
        {
            var query = _mapper.Map<GetCoursePageQuery>(request);
            // var query = new GetCoursePageQuery(
            //     request.InstituteId,
            //     request.Start,
            //     request.Count,
            //     request.Sort,
            //     request.Ascending,
            //     request.CodeFilter,
            //     request.NameFilter,
            //     request.ACFilter,
            //     request.SeasonsFilter,
            //     request.MinimumWorkloadFilter,
            //     request.CreatedByFilter,
            //     request.CreatedFilter,
            //     request.LastModifiedFilter,
            //     request.LastModifiedByFilter
            //     );

            ErrorOr<CoursePageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<CourseResponse>>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var query = new GetCourseByIdQuery(
                id
                );

            ErrorOr<CourseResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<CourseResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseRequest request)
        {
            // var command = _mapper.Map<UpdateCourseCommand>(request);
            var command = new UpdateCourseCommand(
                request.Id,
                request.Name,
                request.AC,
                request.NumberOfSeasons,
                request.MinimumWorkload,
                request.Telephone,
                request.Email,
                request.Image
                );

            ErrorOr<CourseResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
        {
            var command = _mapper.Map<CreateCourseCommand>(request);

            ErrorOr<CourseResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateCourse), _mapper.Map<CourseResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var command = new DeleteCourseCommand(id);

            ErrorOr<CourseResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}