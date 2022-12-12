using PraeceptorCQRS.Contracts.Entities.Holding;

using System.Net;

namespace UserManager.App.Interfaces
{
    public interface IHoldingService
    {
        HttpResponseMessage? GetHttpResponseMessage();

        Task<int> GetHoldingCount();
        Task<HttpResponseMessage> PostPage(GetHoldingPageRequest request);
    }
}
