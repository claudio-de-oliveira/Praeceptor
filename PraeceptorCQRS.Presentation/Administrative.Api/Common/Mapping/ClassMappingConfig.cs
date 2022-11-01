using Mapster;

using PraeceptorCQRS.Application.Entities.Class.Commands;
using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Application.Entities.Class.Queries;
using PraeceptorCQRS.Contracts.Entities.Class;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Common.Mapping
{
    public class ClassMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetClassPageRequest, GetClassPageQuery>();
            config.NewConfig<CreateClassRequest, CreateClassCommand>();
            config.NewConfig<UpdateClassRequest, UpdateClassCommand>();
            config.NewConfig<ClassResult, ClassResponse>()
                .Map(dest => dest, src => src.Class);
            config.NewConfig<ClassPageResult, PageResponse<ClassResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

