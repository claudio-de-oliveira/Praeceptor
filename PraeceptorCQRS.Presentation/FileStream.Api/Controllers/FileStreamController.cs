using ErrorOr;

using FluentValidation;
using FluentValidation.Results;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PraeceptorCQRS.Application.Entities.FileStream.Commands;
using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Entities.FileStream.Queries;
using PraeceptorCQRS.Contracts.Entities.DocumentTemplate;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SqlFileStream;

namespace FileStream.Api.Controllers
{
    [Route("filestream")]
    public class FileStreamController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public FileStreamController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/count/{instituteId}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetFileStreamCountByInstitute(Guid instituteId)
        {
            var query = new GetSqlFileStreamByInstituteCountQuery(
                instituteId
                );

            ErrorOr<SqlFileStreamCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/id/{id}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetFileStreamById(Guid id)
        {
            var query = new GetSqlFileStreamByIdQuery(
                id
                );

            ErrorOr<SqlFileStreamResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<SqlFileStreamResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("exist/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> ExistFileStream(Guid instituteId, string code)
        {
            var query = new ExistSqlFileStreamCodeQuery(
                instituteId,
                code
                );

            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Exist),
                errors => Problem(errors)
                );
        }

        [HttpGet("get/code/{instituteId}/{code}")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetFileStreamByCode(Guid instituteId, string code)
        {
            var query = new GetSqlFileStreamByCodeQuery(
                instituteId,
                code
                );

            ErrorOr<SqlFileStreamResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<SqlFileStreamResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        [Authorize("CreatePolice")]
        public async Task<IActionResult> CreateFileStream(
            [FromBody] CreateSqlFileStreamRequest request,
            [FromServices] IValidator<CreateSqlFileStreamCommand> validator
            )
        {
            var command = _mapper.Map<CreateSqlFileStreamCommand>(request);

            var validatinResult = await validator.ValidateAsync(command);

            if (!validatinResult.IsValid)
            {
                var modelStateDictionary = new ModelStateDictionary();

                foreach (ValidationFailure failure in validatinResult.Errors)
                {
                    modelStateDictionary.AddModelError(
                        failure.PropertyName,
                        failure.ErrorMessage
                        );
                }

                return ValidationProblem(modelStateDictionary);
            }

            ErrorOr<SqlFileStreamResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateFileStream), _mapper.Map<SqlFileStreamResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        [Authorize("DeletePolice")]
        public async Task<IActionResult> DeleteFileStream(Guid id)
        {
            var command = new DeleteSqlFileStreamCommand(id);

            ErrorOr<SqlFileStreamResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("get/page")]
        [Authorize("ReadPolice")]
        public async Task<IActionResult> GetFileStreamPage([FromBody] GetFileStreamPageRequest request)
        {
            var query = _mapper.Map<GetSqlFileStreamPageQuery>(request);

            ErrorOr<SqlFileStreamPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<SqlFileStreamResponse>>(result.Page)),
                errors => Problem(errors)
                );
        }
    }
}
