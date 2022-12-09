using Mapster;

using PraeceptorCQRS.Application.Entities.FileStream.Commands;
using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Entities.FileStream.Queries;
using PraeceptorCQRS.Contracts.Entities.DocumentTemplate;
using PraeceptorCQRS.Contracts.Entities.Page;
using PraeceptorCQRS.Contracts.Entities.SqlFileStream;

namespace FileStream.Api.Common.Mapping
{
    public class FileMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetFilePageRequest, GetFilePageQuery>();

            config.NewConfig<CreateFileRequest, CreateFileCommand>();
            config.NewConfig<FileResult, FileResponse>()
                .Map(dest => dest, src => src.FileStream);
            config.NewConfig<FilePageResult, PageResponse<FileResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}