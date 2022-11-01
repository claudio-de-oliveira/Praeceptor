using PraeceptorCQRS.Contracts.Entities.Holding;

namespace UserManager.App.Interfaces
{
    public interface IHoldingService
    {
        Task<int> GetHoldingCount();
        Task<HttpResponseMessage> PostPage(GetHoldingPageRequest request);
    }
}
