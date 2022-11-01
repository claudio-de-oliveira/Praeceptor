using PraeceptorCQRS.Contracts.Entities.Institute;

namespace UserManager.App.Interfaces
{
    public interface IInstituteService
    {
        Task<int> GetInstituteCount(Guid holdingId);
        Task<HttpResponseMessage> PostPage(GetInstitutePageRequest request);
    }
}
