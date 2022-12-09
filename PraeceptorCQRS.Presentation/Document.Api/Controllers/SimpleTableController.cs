using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.SimpleTable.Commands.CreateCommand;
using PraeceptorCQRS.Application.Entities.SimpleTable.Commands.DeleteCommand;
using PraeceptorCQRS.Application.Entities.SimpleTable.Commands.UpdateCommand;
using PraeceptorCQRS.Application.Entities.SimpleTable.Common;
using PraeceptorCQRS.Application.Entities.SimpleTable.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SimpleTable;
using PraeceptorCQRS.Presentation.Document.Api.Controllers;

namespace Document.Api.Controllers
{
    [Route("table")]
    public class SimpleTableController : ApiController
    {
        protected readonly ISender _mediator;
        protected readonly IMapper _mapper;

        public SimpleTableController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTablesCountByInstitute(Guid instituteId)
        {
            var query = new GetSimpleTableCountQuery(instituteId);

            ErrorOr<SimpleTableCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTablePage([FromBody] GetSimpleTablePageRequest request)
        {
            var query = _mapper.Map<GetSimpleTablePageQuery>(request);

            ErrorOr<SimpleTablePageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<SimpleTableResponse>>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTableById(Guid id)
        {
            var query = new GetSimpleTableByIdQuery(id);

            ErrorOr<SimpleTableResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<SimpleTableResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{code}/{instituteid}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTableByCode(string code, Guid instituteId)
        {
            var query = new GetSimpleTableByCodeQuery(code, instituteId);

            ErrorOr<SimpleTableResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<SimpleTableResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateTable([FromBody] CreateSimpleTableRequest request)
        {
            var command = _mapper.Map<CreateSimpleTableCommand>(request);

            ErrorOr<SimpleTableResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateTable), _mapper.Map<SimpleTableResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("update")]
        // [Authorize("UpdatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateTable([FromBody] UpdateSimpleTableRequest request)
        {
            var command = _mapper.Map<UpdateSimpleTableCommand>(request);

            ErrorOr<SimpleTableResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        // [Authorize("DeletePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTable(Guid id)
        {
            var command = new DeleteSimpleTableCommand(id);

            ErrorOr<SimpleTableResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}