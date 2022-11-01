using Mapster;

using PraeceptorCQRS.Application.Entities.Course.Commands;
using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Application.Entities.Course.Queries;
using PraeceptorCQRS.Contracts.Entities.Course;
using PraeceptorCQRS.Contracts.Entities.Page;

namespace PraeceptorCQRS.Presentation.Administrative.Api.Common.Mapping
{
    public class CourseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetCoursePageRequest, GetCoursePageQuery>();
            config.NewConfig<CreateCourseRequest, CreateCourseCommand>();
            config.NewConfig<UpdateCourseRequest, UpdateCourseCommand>();
            config.NewConfig<CourseResult, CourseResponse>()
                .Map(dest => dest, src => src.Course);
            config.NewConfig<CoursePageResult, PageResponse<CourseResponse>>()
                .Map(dest => dest, src => src.Page);
        }
    }
}

