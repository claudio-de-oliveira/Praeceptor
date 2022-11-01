using PraeceptorCQRS.Application.Entities.DocumentTemplate.Commands;
using PraeceptorCQRS.Application.Entities.DocumentTemplate.Common;
using PraeceptorCQRS.Contracts.Entities.DocumentTemplate;

using Mapster;

namespace PraeceptorCQRS.Presentation.Template.Api.Common.Mapping
{
    public class DocumentTemplateMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateDocumentTemplateRequest, CreateDocumentTemplateCommand>();
            config.NewConfig<UpdateDocumentTemplateRequest, UpdateFileStreamCommand>();
            config.NewConfig<FileStreamResult, DocumentTemplateResponse>()
                .Map(dest => dest, src => src.DocumentTemplate);
        }
    }
}

