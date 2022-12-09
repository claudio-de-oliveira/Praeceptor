using Mapster;

using PraeceptorCQRS.Application.Entities.Document.Commands;
using PraeceptorCQRS.Application.Entities.Document.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Contracts.Entities.Document;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Document.Api.Common.Mapping
{
    public class DocumentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateDocumentRequest, CreateDocumentCommand>();
            config.NewConfig<UpdateDocumentRequest, UpdateDocumentCommand>();
            config.NewConfig<DocumentTextResult, DocumentTextResponse>();
            config.NewConfig<DocumentResult, DocumentResponse>()
                .Map(dest => dest, src => src.Document);
            config.NewConfig<DocumentPageResult, PageResponse<DocumentResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}