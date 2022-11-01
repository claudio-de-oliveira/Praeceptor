using PraeceptorCQRS.Contracts.Entities.Admin;

namespace Administrative.App.Interfaces
{
    public interface IAdminService
    {
        Task<HttpResponseMessage> CreateAdmin(CreateAdminRequest request);
    }
}
