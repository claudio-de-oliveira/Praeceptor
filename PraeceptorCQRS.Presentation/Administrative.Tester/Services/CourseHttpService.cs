using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Course;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Administrative.Tester.Services
{
    internal class CourseHttpService : HttpService
    {
        public CourseHttpService(
            string uri,
            string identityServerURI,
            HttpClient httpClient,
            JsonConverter jsonConverter = null!
            )
            : base(uri, identityServerURI, httpClient, jsonConverter)
        {
        }

        protected override async Task<string> GetAccessToken()
        {
            await Task.CompletedTask;
            return string.Empty;
        }

        public async Task<CourseResponse?> GetCourse(Guid id)
            => await base.GetOne<CourseResponse>("course", "get", "id", id);
        public async Task<HttpResponseMessage> CreateCourse(CreateCourseRequest request)
            => await base.Create<CreateCourseRequest>(request, "course", "create");
        public async Task<HttpResponseMessage> UpdateCourse(UpdateCourseRequest request)
            => await base.Update(request, "course", "update");
        public async Task<HttpResponseMessage> DeleteCourse(Guid id)
            => await base.Delete("course", "delete", id);
    }
}

