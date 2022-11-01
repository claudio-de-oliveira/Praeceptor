using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using FluentValidation;
using FluentValidation.Results;
using ErrorOr;
using MapsterMapper;
using MediatR;

using Serilog;

using PraeceptorCQRS.Application.Entities.DocumentTemplate.Commands;
using PraeceptorCQRS.Application.Entities.DocumentTemplate.Common;
using PraeceptorCQRS.Application.Entities.DocumentTemplate.Queries;
using PraeceptorCQRS.Contracts.Entities.DocumentTemplate;

namespace PraeceptorCQRS.Presentation.Template.Api.Controllers
{
    [Route("documenttemplate")]
    public class DocumentTemplateController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public DocumentTemplateController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get/id/{id}")]
        public async Task<IActionResult> GetDocumentTemplateById(Guid id)
        {
            var query = new GetFileStreamByIdQuery(
                id
                );

            ErrorOr<Application.Entities.DocumentTemplate.Common.FileStreamResult> result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<DocumentTemplateResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDocumentTemplate([FromBody] CreateDocumentTemplateRequest request, [FromServices] IValidator<CreateDocumentTemplateCommand> validator)
        {
            var command = _mapper.Map<CreateDocumentTemplateCommand>(request);

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

            ErrorOr<Application.Entities.DocumentTemplate.Common.FileStreamResult> result = await _mediator.Send(command);

            return result.Match(
                // Use CreatedAtAction to return 201 CreatedAtAction
                result => CreatedAtAction(nameof(CreateDocumentTemplate), _mapper.Map<DocumentTemplateResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDocumentTemplate(Guid id)
        {
            var command = new DeleteFileStreamCommand(id);

            ErrorOr<Application.Entities.DocumentTemplate.Common.FileStreamResult> result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors)
                );
        }
    }
}

