using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;

using ErrorOr;

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
                .MapWith(source => MapCourseResultToCourseResponse(source));
            config.NewConfig<CoursePageResult, PageResponse<CourseResponse>>()
                .MapWith(source => MapCoursePageResultToCourseResponse(source));
            // .Map(dest => dest, src => src.Page);
        }

        private static PageResponse<CourseResponse> MapCoursePageResultToCourseResponse(CoursePageResult source)
        {
            List<CourseResponse> entities = new();

            foreach (var src in source.Page.Entities)
            {
                entities.Add(
                    new(
                        src.Id,
                        src.Code,
                        src.Name,
                        src.CEO,
                        src.AC,
                        src.NumberOfSeasons,
                        src.MinimumWorkload,
                        src.Telephone,
                        src.Email,
                        src.Image,
                        src.InstituteId,
                        src.Created,
                        src.CreatedBy,
                        src.LastModified,
                        src.LastModifiedBy
                        )
                    );
            }

            return new PageResponse<CourseResponse>(
                source.Page.CurrentPage,
                source.Page.Size,
                source.Page.PreviousPage,
                source.Page.NextPage,
                source.Page.NumberOfPages,
                entities
                );
        }

        private static CourseResponse MapCourseResultToCourseResponse(CourseResult source)
            => new(
                source.Course.Id,
                source.Course.Code,
                source.Course.Name,
                source.Course.CEO,
                source.Course.AC,
                source.Course.NumberOfSeasons,
                source.Course.MinimumWorkload,
                source.Course.Telephone,
                source.Course.Email,
                source.Course.Image,
                source.Course.InstituteId,
                source.Course.Created,
                source.Course.CreatedBy,
                source.Course.LastModified,
                source.Course.LastModifiedBy
                );
    }
}