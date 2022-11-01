using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.Holding;

namespace Administrative.App.Interfaces
{
    public interface IHoldingService
    {
        Task<int> GetHoldingCount();
        Task<HttpResponseMessage> PostPage(GetHoldingPageRequest request);
        Task<HoldingModel?> GetHoldingById(Guid id);
        Task<HoldingModel?> GetHoldingByCode(string code);
        Task<HttpResponseMessage> UpdateHolding(UpdateHoldingRequest request);
        Task<HttpResponseMessage> CreateHolding(CreateHoldingRequest request);
        Task<HttpResponseMessage> DeleteHolding(Guid id);
    }
}
