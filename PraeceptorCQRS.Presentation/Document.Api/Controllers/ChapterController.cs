using Document.Api.Controllers;

using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Chapter.Commands;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Entities.Chapter.Queries;
using PraeceptorCQRS.Contracts.Entities;
using PraeceptorCQRS.Contracts.Entities.Chapter;
using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Document.Api.Controllers
{
    [Route("chapter")]
    public class ChapterController : ListEndPoints
    {
        public ChapterController(ISender mediator, IMapper mapper)
            : base(mediator, mapper)
        {
            // Nothing more todo
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetChapterCountByInstitute(Guid instituteId)
        {
            var query = new GetChapterByInstituteCountQuery(
                instituteId
                );

            ErrorOr<ChapterCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/list/{documentId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetChapterListByDocument(Guid documentId)
        {
            var query = new GetChaptersInDocumentListQuery(
                documentId
                );

            ErrorOr<ChapterListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<ChapterResponse>>(result.Chapters)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/all/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetChapterByInstitute(Guid instituteId)
        {
            var query = new GetChapterByInstituteQuery(
                instituteId
                );

            ErrorOr<ChapterListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<ChapterResponse>>(result.Chapters)),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetChapterPage([FromBody] GetPageRequest request)
        {
            var query = new GetChapterPageQuery(
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

            ErrorOr<ChapterPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<ChapterResponse>>(result/*.Page*/)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/page/{instituteId}/{start}/{count}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetChapterPageByInstitute(Guid instituteId, int start, int count)
        {
            var query = new GetChapterByInstitutePageQuery(
                instituteId,
                start,
                count
                );

            ErrorOr<ChapterListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<ChapterResponse>>(result.Chapters)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetChapterById(Guid id)
        {
            var query = new GetChapterByIdQuery(
                id
                );

            ErrorOr<ChapterResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<ChapterResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateChapter([FromBody] UpdateChapterRequest request)
        {
            var command = _mapper.Map<UpdateChapterCommand>(request);

            ErrorOr<ChapterResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateChapter([FromBody] CreateChapterRequest request)
        {
            var command = _mapper.Map<CreateChapterCommand>(request);

            ErrorOr<ChapterResult> result = await _mediator.Send(command);

            // NÃO ESTÁ RETORNANDO O CÓDIGO 201 (CREATED). RETORNA 200 (OK)
            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateChapter), _mapper.Map<ChapterResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteChapter(Guid id)
        {
            var command = new DeleteChapterCommand(id);

            ErrorOr<ChapterResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        #region L I S T   E N D P O I N T S

        [HttpPost("section/create/first")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateFirstSection([FromBody] CreateFirstNodeRequest request)
        {
            var result = await base.CreateFirstNode(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateFirstSection), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("section/move/forward/{parentId}/{position}")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> MoveSectionForward(Guid parentId, Guid position)
        {
            var result = await base.MoveForward(parentId, position);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPut("section/move/backward/{parentId}/{position}")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> MoveSectionBackward(Guid parentId, Guid position)
        {
            var result = await base.MoveBackward(parentId, position);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("section/insert/after")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> InsertSectionAfterPosition([FromBody] InsertNodeRequest request)
        {
            var result = await base.InsertNodeAfterPosition(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(InsertSectionAfterPosition), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("section/insert/before")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> InsertSectionBeforePosition([FromBody] InsertNodeRequest request)
        {
            var result = await base.InsertNodeBeforePosition(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(InsertSectionBeforePosition), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("section/get/first/{documentId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetFirstSectionPosition(Guid documentId)
        {
            var result = await base.GetFirstNodePosition(documentId, documentId);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("section/get/next/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetNextSectionPosition(Guid id)
        {
            var result = await base.GetNextNodePosition(id);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("section/get/last/{documentId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetLastSectionPosition(Guid documentId)
        {
            var result = await base.GetLastNodePosition(documentId, documentId);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("section/get/previous/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreviousSectionPosition(Guid id)
        {
            var result = await base.GetPreviousNodePosition(id);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("section/delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteSectionAt(Guid id)
        {
            var result = await base.DeleteNodeAt(id);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        #endregion L I S T   E N D P O I N T S
    }
}