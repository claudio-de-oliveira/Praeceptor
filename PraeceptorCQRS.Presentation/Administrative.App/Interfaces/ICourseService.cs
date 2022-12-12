using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.Course;

using System.Net;

namespace Administrative.App.Interfaces
{
    public interface ICourseService
    {
        HttpResponseMessage? GetHttpResponseMessage();

        Task<int> GetCourseCount(Guid instituteId);
        Task<HttpResponseMessage> PostPage(GetCoursePageRequest request);
        Task<CourseModel?> GetCourseById(Guid id);
        Task<CourseModel?> GetCourseByCode(string code);
        Task<HttpResponseMessage> UpdateCourse(UpdateCourseRequest request);
        Task<HttpResponseMessage> CreateCourse(CreateCourseRequest request);
        Task<HttpResponseMessage> DeleteCourse(Guid id);
    }
}
