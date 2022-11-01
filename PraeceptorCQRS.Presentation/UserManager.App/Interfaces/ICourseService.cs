using PraeceptorCQRS.Contracts.Entities.Course;

namespace UserManager.App.Interfaces
{
    public interface ICourseService
    {
        Task<int> GetCourseCount(Guid instituteId);
        Task<HttpResponseMessage> PostPage(GetCoursePageRequest request);
    }
}
