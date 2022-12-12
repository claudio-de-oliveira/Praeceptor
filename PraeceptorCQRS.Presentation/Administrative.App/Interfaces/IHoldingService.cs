using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.Holding;

using System.Net;

namespace Administrative.App.Interfaces
{
    public interface IHoldingService
    {
        HttpResponseMessage? GetHttpResponseMessage();

        Task<int> GetHoldingCount();
        Task<HttpResponseMessage> PostPage(GetHoldingPageRequest request);
        Task<HoldingModel?> GetHoldingById(Guid id);
        Task<HoldingModel?> GetHoldingByCode(string code);
        Task<HttpResponseMessage> UpdateHolding(UpdateHoldingRequest request);
        Task<HttpResponseMessage> CreateHolding(CreateHoldingRequest request);
        Task<HttpResponseMessage> DeleteHolding(Guid id);
    }
}
