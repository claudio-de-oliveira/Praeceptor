using Ardalis.GuardClauses;

using MapsterMapper;

using ErrorOr;

using Hangfire;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Application.Entities.ToWord.Commands;
using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SqlDocxStream;
using PraeceptorCQRS.Contracts.Entities.ToWord;

namespace DocumentToWord.Api.Controllers
{
    [Route("api/toword")]
    public class ToWordController : ApiController
    {
        protected readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ToWordController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("docx/count/{instituteId}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDocxStreamCountByInstitute(Guid instituteId)
        {
            var query = new GetSqlDocxStreamByInstituteCountQuery(
                instituteId
                );

            ErrorOr<SqlDocxStreamCountResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(result.Count),
                errors => Problem(errors)
                );
        }

        [HttpGet("docx/id/{id}")]
        // [Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDocxStreamById(Guid id)
        {
            var query = new GetSqlDocxStreamByIdQuery(
                id
                );

            ErrorOr<DocxResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<DocxResponse>(result)),
                errors => Problem(errors)
                );
        }

        // [HttpGet("exist/{instituteId}/{code}")]
        // // [Authorize("ReadPolice")]
        // [AllowAnonymous]
        // public async Task<IActionResult> ExistDocxStream(Guid instituteId, string code)
        // {
        //     var query = new ExistSqlDocxStreamCodeQueryXXX(
        //         instituteId,
        //         code
        //         );
        // 
        //     var result = await _mediator.Send(query);
        // 
        //     return result.Match(
        //         result => Ok(result.Exist),
        //         errors => Problem(errors)
        //         );
        // }

        // [HttpGet("docx/code/{instituteId}/{code}")]
        // // [Authorize("ReadPolice")]
        // [AllowAnonymous]
        // public async Task<IActionResult> GetDocxStreamByCode(Guid instituteId, string code)
        // {
        //     var query = new GetSqlDocxStreamByCodeQueryXXX(
        //         instituteId,
        //         code
        //         );
        // 
        //     ErrorOr<DocxResult> result = await _mediator.Send(query);
        // 
        //     return result.Match(
        //         result => Ok(_mapper.Map<DocxResponse>(result)),
        //         errors => Problem(errors)
        //         );
        // }

        [HttpDelete("delete/{id}")]
        // [Authorize("DeletePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteDocxStream(Guid id)
        {
            var command = new DeleteSqlDocxStreamCommand(id);

            ErrorOr<DocxResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpPost("docx/page")]
        //[Authorize("ReadPolice")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDocxStreamPage([FromBody] GetDocxPageRequest request)
        {
            var query = _mapper.Map<GetDocxPageQuery>(request);

            ErrorOr<DocxPageResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<PageResponse<DocxResponse>>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateDocxStream([FromBody] ConvertPpcToDocxRequest request)
        {
            Guard.Against.Null(request);
            await Task.CompletedTask;

            var command = new CreateDocxCommand(
                request.CourseId,
                request.Curriculum,
                request.Description,
                request.DocumentId,
                request.TemplateId,
                Guid.NewGuid(),
                request.GroupValues,
                request.CreatedBy
                );

            BackgroundJob.Enqueue(() => ConvertToWordJob(command));

            return Ok(command.FileId);
        }

        // public void SaveDocumentJob()
        // {
        //     Console.WriteLine("SALVANDO DOCUMENTO WORD");
        //     if (ppcToWordResult.IsError)
        //     {
        //         string fileDownloadName = "D:\\users\\clalu\\Source\\repos\\testeB\\PraeceptorCQRS\\Erro.docx";
        //
        //         var memory = new MemoryStream();
        //         using (var stream = new FileStream(fileDownloadName, FileMode.Open))
        //         {
        //             stream.CopyToAsync(memory).GetAwaiter().GetResult();
        //         }
        //         memory.Position = 0;
        //
        //         // return File(memory, "application/vnd.ms-word", Path.GetFileName(fileDownloadName));
        //     }
        // }

        [HttpPost("pea")]
        // [Authorize("CreatePolice")]
        [AllowAnonymous]
        public async Task<ActionResult> ConvertPeaToWord([FromBody] ConvertPeaToDocxRequest request)
        {
            Guard.Against.Null(request);

            var query = new GetPeaTextByIdQuery(
                request.PeaId,
                request.Season
                );

            ErrorOr<PlannerTextResult> result = await _mediator.Send(query);

            if (result.IsError)
                return null!;

            string fileDownloadName = "D:\\Download\\Resume_CV_.pdf";

            var memory = new MemoryStream();
            using (var stream = new FileStream(fileDownloadName, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "application/vnd.ms-word", Path.GetFileName(fileDownloadName));
        }

        private async Task<string> ConvertPeaToText(Guid peaId, int season)
        {
            var query = new GetPeaTextByIdQuery(
                peaId,
                season
                );

            ErrorOr<PlannerTextResult> result = await _mediator.Send(query);

            if (result.IsError)
                return "";
            else
                return result.Value.Text;
        }

        public void ConvertToWordJob(CreateDocxCommand command)
        {
            Console.WriteLine("GERANDO DOCUMENTO WORD");
            _mediator.Send(command).GetAwaiter().GetResult();
        }
    }
}