using Mapster;

using PraeceptorCQRS.Application.Entities.FileStream.Commands;
using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Entities.FileStream.Queries;
using PraeceptorCQRS.Contracts.Entities.DocumentTemplate;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SqlFileStream;

namespace FileStream.Api.Common.Mapping
{
    public class DocumentTemplateMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetFileStreamPageRequest, GetSqlFileStreamPageQuery>();

            config.NewConfig<CreateSqlFileStreamRequest, CreateSqlFileStreamCommand>()
                .Map(target => target.Name, source => source.Name)
                .Map(target => target.Title, source => source.Title)
                .Map(target => target.Source, source => source.Source)
                .Map(target => target.Description, source => source.Description)
                .Map(target => target.ContentType, source => source.ContentType)
                .Map(target => target.InstituteId, source => source.InstituteId)
                .Map(target => target.Data, source => source.Data);
            config.NewConfig<SqlFileStreamResult, SqlFileStreamResponse>()
                .Map(dest => dest, src => src.FileStream);
            config.NewConfig<SqlFileStreamPageResult, PageResponse<SqlFileStreamResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}
