using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Component.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.Component.Commands.DeleteCommand;
using PraeceptorCQRS.Application.Entities.Component.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Entities.Component.Queries;
using PraeceptorCQRS.Contracts.Entities.Component;
using PraeceptorCQRS.Presentation.Administrative.Api.Controllers;

namespace Administrative.Api.Controllers
{
    [Route("component")]
    public class ComponentController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ComponentController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateComponent([FromBody] CreateComponentRequest request)
        {
            // var command = _mapper.Map<CreateComponentCommand>(request);
            var command = new CreateComponentCommand(
                request.CourseId,
                request.Curriculum,
                request.Season,
                request.ClassId,
                request.AxisTypeId,
                request.Optative
                );

            ErrorOr<ComponentResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateComponent), _mapper.Map<ComponentResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateComponent([FromBody] UpdateComponentRequest request)
        {
            var command = _mapper.Map<UpdateComponentCommand>(request);
            // var command = new UpdateComponentCommand(
            //     request.CourseId,
            //     request.Curriculum,
            //     request.Season,
            //     request.ClassId,
            //     request.Optative,
            //     request.AxisTypeId
            //     );

            ErrorOr<ComponentResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/curriculum/{courseId}/{curriculum}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetComponentListByCourseAndCurriculum(Guid courseId, int curriculum)
        {
            var query = new GetComponentByCourseAndCurriculumQuery(
                courseId,
                curriculum
                );

            ErrorOr<ComponentListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<ComponentResponse>>(result.Components)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/season/{courseId}/{curriculum}/{season}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetComponentListByCourseAndCurriculumAndSeason(Guid courseId, int curriculum, int season)
        {
            var query = new GetComponentByCourseAndCurriculumAndSeasonQuery(
                courseId,
                curriculum,
                season
                );

            ErrorOr<ComponentListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<ComponentResponse>>(result.Components)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/class/{courseId}/{curriculum}/{classId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetComponentByCourseAndCurriculumAndClass(Guid courseId, int curriculum, Guid classId)
        {
            var query = new GetComponentByCourseAndCurriculumAndClassQuery(
                courseId,
                curriculum,
                classId
                );

            ErrorOr<ComponentResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<ComponentResponse>(result.Component)),
                errors => Problem(errors)
                );
        }

        [HttpGet("list/curriculum/{courseId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetCurriculumsByCourseId(Guid courseId)
        {
            var query = new GetCurriculumsByCourseIdQuery(
                courseId
                );

            ErrorOr<CurriculumListResult> result = await _mediator.Send(query);

            var curriculumList = _mapper.Map<List<CurriculumResponse>>(result.Value.Curriculums);

            return result.Match(
                result => Ok(curriculumList),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{courseId}/{curriculum}/{classId}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteClass(Guid courseId, int curriculum, Guid classId)
        {
            var command = new DeleteComponentCommand(courseId, curriculum, classId);

            ErrorOr<ComponentResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}