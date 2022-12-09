using Document.Api.Controllers;

using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.Document.Commands;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Entities.Document.Queries;
using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Queries;
using PraeceptorCQRS.Contracts.Entities;
using PraeceptorCQRS.Contracts.Entities.Document;
using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Document.Api.Controllers
{
    [Route("document")]
    public class DocumentController : ListEndPoints
    {
        public DocumentController(ISender mediator, IMapper mapper)
            : base(mediator, mapper)
        {
            // Nothing more todo
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetDocumentCountByInstitute(Guid instituteId)
        {
            var query = new GetDocumentByInstituteCountQuery(
                instituteId
                );

            ErrorOr<DocumentCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/all/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetDocumentByInstitute(Guid instituteId)
        {
            var query = new GetDocumentByInstituteQuery(
                instituteId
                );

            ErrorOr<DocumentListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<DocumentResponse>>(result.Documents)),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetDocumentPage([FromBody] GetPageRequest request)
        {
            var query = new GetDocumentPageQuery(
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

            ErrorOr<DocumentPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<DocumentResponse>>(result/*.Page*/)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/page/{instituteId}/{start}/{count}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetDocumentPageByInstitute(Guid instituteId, int start, int count)
        {
            var query = new GetDocumentByInstitutePageQuery(
                instituteId,
                start,
                count
                );

            ErrorOr<DocumentListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<DocumentResponse>>(result.Documents)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetDocumentById(Guid id)
        {
            var query = new GetDocumentByIdQuery(
                id
                );

            ErrorOr<DocumentResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<DocumentResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/text/{id}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDocumentTextById(Guid id)
        {
            var query = new GetDocumentTextByIdQuery(
                id
                );

            ErrorOr<DocumentTextResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<DocumentTextResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateDocument([FromBody] UpdateDocumentRequest request)
        {
            var command = _mapper.Map<UpdateDocumentCommand>(request);

            ErrorOr<DocumentResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateDocument([FromBody] CreateDocumentRequest request)
        {
            var command = _mapper.Map<CreateDocumentCommand>(request);
            // var command = new CreateDocumentCommand(
            //     request.Title,
            //     request.Text,
            //     request.InstituteId,
            //     null
            //     );

            ErrorOr<DocumentResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateDocument), _mapper.Map<DocumentResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var command = new DeleteDocumentCommand(id);

            ErrorOr<DocumentResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        #region L I S T   E N D P O I N T S

        [HttpPost("chapter/create/first")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateFirstChapter(
            [FromBody] CreateFirstNodeRequest request
            )
        {
            var result = await base.CreateFirstNode(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateFirstChapter), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("chapter/move/forward/{parentId}/{position}")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> MoveChapterForward(Guid parentId, Guid position)
        {
            var result = await base.MoveForward(parentId, position);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPut("chapter/move/backward/{parentId}/{position}")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> MoveChapterBackward(Guid parentId, Guid position)
        {
            var result = await base.MoveBackward(parentId, position);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("chapter/insert/after")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> InsertChapterAfterPosition(
            [FromBody] InsertNodeRequest request
            )
        {
            var result = await base.InsertNodeAfterPosition(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(InsertChapterAfterPosition), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("chapter/insert/before")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> InsertChapterBeforePosition(
            [FromBody] InsertNodeRequest request
            )
        {
            var result = await base.InsertNodeBeforePosition(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(InsertNodeBeforePosition), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("chapter/get/first/{documentId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetFirstChapterPosition(Guid documentId)
        {
            var result = await base.GetFirstNodePosition(documentId, documentId);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("chapter/get/next/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetNextChapterPosition(Guid id)
        {
            var result = await base.GetNextNodePosition(id);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("chapter/get/last/{documentId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetLastChapterPosition(Guid documentId)
        {
            var result = await base.GetLastNodePosition(documentId, documentId);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("chapter/get/previous/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreviousChapterPosition(Guid id)
        {
            var result = await base.GetPreviousNodePosition(id);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("chapter/delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteChapterAt(Guid id)
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