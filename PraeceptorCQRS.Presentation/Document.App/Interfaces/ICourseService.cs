using PraeceptorCQRS.Contracts.Entities.Course;

using System.Net;

namespace Document.App.Interfaces
{
    public interface ICourseService
    {
        HttpResponseMessage? GetHttpResponseMessage();

        Task<HttpResponseMessage> PostPage(GetCoursePageRequest request);
    }
}