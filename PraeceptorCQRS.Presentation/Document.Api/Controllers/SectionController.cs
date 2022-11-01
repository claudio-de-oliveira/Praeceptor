using Document.Api.Controllers;

using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PraeceptorCQRS.Application.Entities.Section.Commands;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Entities.Section.Queries;
using PraeceptorCQRS.Contracts.Entities;
using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.Section;

namespace PraeceptorCQRS.Presentation.Document.Api.Controllers
{
    [Route("section")]
    public class SectionController : ListEndPoints
    {
        public SectionController(ISender mediator, IMapper mapper)
            : base(mediator, mapper)
        {
            // Nothing more todo
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSectionCountByInstitute(Guid instituteId)
        {
            var query = new GetSectionByInstituteCountQuery(
                instituteId
                );

            ErrorOr<SectionCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/all/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSectionByInstitute(Guid instituteId)
        {
            var query = new GetSectionByInstituteQuery(
                instituteId
                );

            ErrorOr<SectionListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SectionResponse>>(result.Sections)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/list/{chapterId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSectionListByChapter(Guid chapterId)
        {
            var query = new GetSectionsInChapterListQuery(
                chapterId
                );

            ErrorOr<SectionListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SectionResponse>>(result.Sections)),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSectionPage([FromBody] GetPageRequest request)
        {
            var query = new GetSectionPageQuery(
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

            ErrorOr<SectionPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<SectionResponse>>(result.Page)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/page/{instituteId}/{start}/{count}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSectionPageByInstitute(Guid instituteId, int start, int count)
        {
            var query = new GetSectionByInstitutePageQuery(
                instituteId,
                start,
                count
                );

            ErrorOr<SectionListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SectionResponse>>(result.Sections)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSectionById(Guid id)
        {
            var query = new GetSectionByIdQuery(
                id
                );

            ErrorOr<SectionResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<SectionResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateSection([FromBody] UpdateSectionRequest request)
        {
            var command = _mapper.Map<UpdateSectionCommand>(request);

            ErrorOr<SectionResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateSection([FromBody] CreateSectionRequest request)
        {
            var command = _mapper.Map<CreateSectionCommand>(request);

            ErrorOr<SectionResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateSection), _mapper.Map<SectionResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var command = new DeleteSectionCommand(id);

            ErrorOr<SectionResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }


        #region L I S T   E N D P O I N T S
        [HttpPost("subsection/create/first")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateFirstSubSection([FromBody] CreateFirstNodeRequest request)
        {
            var result = await base.CreateFirstNode(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateFirstSubSection), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("subsection/move/forward/{parentId}/{position}")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> MoveSubSectionForward(Guid parentId, Guid position)
        {
            var result = await base.MoveForward(parentId, position);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPut("subsection/move/backward/{parentId}/{position}")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> MoveSubSectionBackward(Guid parentId, Guid position)
        {
            var result = await base.MoveBackward(parentId, position);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("subsection/insert/after")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> InsertSubSectionAfterPosition([FromBody] InsertNodeRequest request)
        {
            var result = await base.InsertNodeAfterPosition(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(InsertSubSectionAfterPosition), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("subsection/insert/before")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> InsertSubSectionBeforePosition([FromBody] InsertNodeRequest request)
        {
            var result = await base.InsertNodeBeforePosition(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(InsertSubSectionBeforePosition), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("subsection/get/first/{documentId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetFirstSubSectionPosition(Guid documentId)
        {
            var result = await base.GetFirstNodePosition(documentId, documentId);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("subsection/get/next/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetNextSubSectionPosition(Guid id)
        {
            var result = await base.GetNextNodePosition(id);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("subsection/get/last/{documentId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetLastSubSectionPosition(Guid documentId)
        {
            var result = await base.GetLastNodePosition(documentId, documentId);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("subsection/get/previous/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreviousSubSectionPosition(Guid id)
        {
            var result = await base.GetPreviousNodePosition(id);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("subsection/delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteSubSectionAt(Guid id)
        {
            var result = await base.DeleteNodeAt(id);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
        #endregion
    }
}

