using Mapster;

using PraeceptorCQRS.Application.Entities.ToWord.Commands;
using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Queries;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SqlDocxStream;

namespace DocumentToWord.Api.Common.Mapping
{
    public class DocxMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetDocxPageRequest, GetDocxPageQuery>();
            config.NewConfig<CreateDocxRequest, CreateDocxCommand>();
            config.NewConfig<DocxResult, DocxResponse>()
                .Map(dest => dest, src => src.DocxStream);
            config.NewConfig<DocxPageResult, PageResponse<DocxResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}