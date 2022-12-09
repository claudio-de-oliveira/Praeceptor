using PraeceptorCQRS.Contracts.Entities.Course;

namespace Document.App.Interfaces
{
    public interface ICourseService
    {
        Task<HttpResponseMessage> PostPage(GetCoursePageRequest request);
    }
}