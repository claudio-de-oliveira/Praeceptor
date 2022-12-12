using PraeceptorCQRS.Contracts.Entities.Course;

using System.Net;

namespace UserManager.App.Interfaces
{
    public interface ICourseService
    {
        HttpResponseMessage? GetHttpResponseMessage();

        Task<int> GetCourseCount(Guid instituteId);
        Task<HttpResponseMessage> PostPage(GetCoursePageRequest request);
    }
}
