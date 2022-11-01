using Document.Api.Controllers;

using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.SubSection.Commands;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Entities.SubSection.Queries;
using PraeceptorCQRS.Contracts.Entities;
using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SubSection;

namespace PraeceptorCQRS.Presentation.Document.Api.Controllers
{
    [Route("subsection")]
    public class SubSectionController : ListEndPoints
    {
        public SubSectionController(ISender mediator, IMapper mapper)
            : base(mediator, mapper)
        {
            // Nothing more todo
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSectionCountByInstitute(Guid instituteId)
        {
            var query = new GetSubSectionByInstituteCountQuery(
                instituteId
                );

            ErrorOr<SubSectionCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/all/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSectionByInstitute(Guid instituteId)
        {
            var query = new GetSubSectionByInstituteQuery(
                instituteId
                );

            ErrorOr<SubSectionListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SubSectionResponse>>(result.SubSections)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/list/{sectionId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSectionListBySection(Guid sectionId)
        {
            var query = new GetSubSectionsInSectionListQuery(
                sectionId
                );

            ErrorOr<SubSectionListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SubSectionResponse>>(result.SubSections)),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSectionPage([FromBody] GetPageRequest request)
        {
            var query = new GetSubSectionPageQuery(
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

            ErrorOr<SubSectionPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<SubSectionResponse>>(result.Page)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/page/{instituteId}/{start}/{count}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSectionPageByInstitute(Guid instituteId, int start, int count)
        {
            var query = new GetSubSectionByInstitutePageQuery(
                instituteId,
                start,
                count
                );

            ErrorOr<SubSectionListResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<SubSectionResponse>>(result.SubSections)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetSubSectionById(Guid id)
        {
            var query = new GetSubSectionByIdQuery(
                id
                );

            ErrorOr<SubSectionResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<SubSectionResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> UpdateSubSection([FromBody] UpdateSubSectionRequest request)
        {
            var command = _mapper.Map<UpdateSubSectionCommand>(request);

            ErrorOr<SubSectionResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateSubSection([FromBody] CreateSubSectionRequest request)
        {
            var command = _mapper.Map<CreateSubSectionCommand>(request);

            ErrorOr<SubSectionResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateSubSection), _mapper.Map<SubSectionResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteSubSection(Guid id)
        {
            var command = new DeleteSubSectionCommand(id);

            ErrorOr<SubSectionResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }


        #region L I S T   E N D P O I N T S
        [HttpPost("subsubsection/create/first")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateFirstSubSubSection([FromBody] CreateFirstNodeRequest request)
        {
            var result = await base.CreateFirstNode(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateFirstSubSubSection), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("subsubsection/move/forward/{parentId}/{position}")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> MoveSubSubSectionForward(Guid parentId, Guid position)
        {
            var result = await base.MoveForward(parentId, position);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPut("subsubsection/move/backward/{parentId}/{position}")]
        [Authorize("UpdatePolice")]
        public async Task<IActionResult> MoveSubSubSectionBackward(Guid parentId, Guid position)
        {
            var result = await base.MoveBackward(parentId, position);
            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("subsubsection/insert/after")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> InsertSubSubSectionAfterPosition([FromBody] InsertNodeRequest request)
        {
            var result = await base.InsertNodeAfterPosition(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(InsertSubSubSectionAfterPosition), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("subsubsection/insert/before")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> InsertSubSubSectionBeforePosition([FromBody] InsertNodeRequest request)
        {
            var result = await base.InsertNodeBeforePosition(request);
            return result.Value.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(InsertSubSubSectionBeforePosition), _mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("subsubsection/get/first/{documentId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetFirstSubSubSectionPosition(Guid documentId)
        {
            var result = await base.GetFirstNodePosition(documentId, documentId);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("subsubsection/get/next/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetNextSubSubSectionPosition(Guid id)
        {
            var result = await base.GetNextNodePosition(id);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("subsubsection/get/last/{documentId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetLastSubSubSectionPosition(Guid documentId)
        {
            var result = await base.GetLastNodePosition(documentId, documentId);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("subsubsection/get/previous/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetPreviousSubSubSectionPosition(Guid id)
        {
            var result = await base.GetPreviousNodePosition(id);
            return result.Match(
                result => Ok(_mapper.Map<NodeResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("subsubsection/delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteSubSubSectionAt(Guid id)
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

