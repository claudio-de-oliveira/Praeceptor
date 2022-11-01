using Mapster;

using PraeceptorCQRS.Application.Entities.Chapter.Commands;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Contracts.Entities.Chapter;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Document.Api.Common.Mapping
{
    public class ChapterMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateChapterRequest, CreateChapterCommand>();
            config.NewConfig<UpdateChapterRequest, UpdateChapterCommand>();
            config.NewConfig<ChapterResult, ChapterResponse>()
                .Map(dest => dest, src => src.Chapter);
            config.NewConfig<ChapterPageResult, PageResponse<ChapterResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

