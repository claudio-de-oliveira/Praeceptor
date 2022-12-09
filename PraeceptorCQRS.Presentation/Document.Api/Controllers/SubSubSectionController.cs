using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.SubSubSection.Commands;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Entities.SubSubSection.Queries;
using PraeceptorCQRS.Contracts.Entities;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SubSubSection;

namespace PraeceptorCQRS.Presentation.Document.Api.Controllers
{
    [Route("subsubsection")]
    public class SubSubSectionController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public SubSubSectionController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSubSectionCountByInstitute(Guid instituteId)
        {
            var query = new GetSubSubSectionByInstituteCountQuery(
                instituteId
                );

            ErrorOr<SubSubSectionCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/all/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSubSectionByInstitute(Guid instituteId)
        {
            var query = new GetSubSubSectionByInstituteQuery(
                instituteId
                );

            ErrorOr<SubSubSectionListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SubSubSectionResponse>>(result.SubSubSections)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/list/{subsectionId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSubSectionListBySubSection(Guid subsectionId)
        {
            var query = new GetSubSubSectionsInSubSectionListQuery(
                subsectionId
                );

            ErrorOr<SubSubSectionListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SubSubSectionResponse>>(result.SubSubSections)),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSubSectionPage([FromBody] GetPageRequest request)
        {
            var query = new GetSubSubSectionPageQuery(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.TitleFilter,
                request.TextFilter,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            ErrorOr<SubSubSectionPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<SubSubSectionResponse>>(result/*.Page*/)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/page/{instituteId}/{start}/{count}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSubSectionPageByInstitute(Guid instituteId, int start, int count)
        {
            var query = new GetSubSubSectionByInstitutePageQuery(
                instituteId,
                start,
                count
                );

            ErrorOr<SubSubSectionListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SubSubSectionResponse>>(result.SubSubSections)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSubSectionById(Guid id)
        {
            var query = new GetSubSubSectionByIdQuery(
                id
                );

            ErrorOr<SubSubSectionResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<SubSubSectionResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateSubSubSection([FromBody] UpdateSubSubSectionRequest request)
        {
            var command = _mapper.Map<UpdateSubSubSectionCommand>(request);

            ErrorOr<SubSubSectionResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateSubSubSection([FromBody] CreateSubSubSectionRequest request)
        {
            var command = _mapper.Map<CreateSubSubSectionCommand>(request);

            ErrorOr<SubSubSectionResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateSubSubSection), _mapper.Map<SubSubSectionResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteSubSubSection(Guid id)
        {
            var command = new DeleteSubSubSectionCommand(id);

            ErrorOr<SubSubSectionResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}

