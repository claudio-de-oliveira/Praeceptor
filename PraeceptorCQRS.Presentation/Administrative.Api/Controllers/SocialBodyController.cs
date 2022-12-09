using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.SocialBody.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.SocialBody.Commands.DeleteCommand;
using PraeceptorCQRS.Application.Entities.SocialBody.Common;
using PraeceptorCQRS.Application.Entities.SocialBody.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SocialBody;
using PraeceptorCQRS.Presentation.Administrative.Api.Controllers;

namespace Administrative.Api.Controllers
{
    [Route("socialbody")]
    public class SocialBodyController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public SocialBodyController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateSocialBodyEntry([FromBody] CreateSocialBodyEntryRequest request)
        {
            var command = _mapper.Map<CreateSocialBodyEntryCommand>(request);

            ErrorOr<SocialBodyEntryResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateSocialBodyEntry), _mapper.Map<SocialBodyEntryResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/count/{courseId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSocialBodyEntriesCountByCourse(Guid courseId)
        {
            var query = new GetSocialBodyEntriesCountByCourseQuery(
                courseId
                );

            ErrorOr<SocialBodyEntriesCount> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/list/{courseId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSocialBodyEntriesList(Guid courseId)
        {
            var query = new GetSocialBodyEntriesByCourseQuery(courseId);

            ErrorOr<SocialBodyEntryListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SocialBodyEntryResponse>>(result.List)),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSocialBodyEntriesPage([FromBody] GetSocialBodyPageRequest request)
        {
            var query = _mapper.Map<GetSocialBodyEntryPageQuery>(request);

            ErrorOr<SocialBodyEntryPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<SocialBodyEntryResponse>>(result/*.Page*/)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/entry/{courseId}/{preceptorId}/{roleId}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSocialBodyEntry(Guid courseId, Guid preceptorId, Guid roleId)
        {
            var query = new GetSocialBodyEntryQuery(
                courseId,
                preceptorId,
                roleId
                );

            ErrorOr<SocialBodyEntryResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<SocialBodyEntryResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/entry/{courseId}/{preceptorId}/{roleId}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteSocialBodyEntry(Guid courseId, Guid preceptorId, Guid roleId)
        {
            var command = new DeleteSocialBodyEntryCommand(courseId, preceptorId, roleId);

            ErrorOr<bool> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{courseId}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteSocialBodyByCourse(Guid courseId)
        {
            var command = new DeleteSocialBodyEntryByCourseCommand(courseId);

            ErrorOr<bool> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}