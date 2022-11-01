using Mapster;

using PraeceptorCQRS.Application.Entities.ClassType.Commands;
using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Application.Entities.ClassType.Queries;
using PraeceptorCQRS.Contracts.Entities.ClassType;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Common.Mapping
{
    public class ClassTypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetClassTypePageRequest, GetClassTypePageQuery>();
            config.NewConfig<CreateClassTypeRequest, CreateClassTypeCommand>();
            config.NewConfig<UpdateClassTypeRequest, UpdateClassTypeCommand>();
            config.NewConfig<ClassTypeResult, ClassTypeResponse>()
                .Map(dest => dest, src => src.ClassType);
            config.NewConfig<ClassTypePageResult, PageResponse<ClassTypeResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

