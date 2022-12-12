using PraeceptorCQRS.Contracts.Entities.Admin;

using System.Net;

namespace Administrative.App.Interfaces
{
    public interface IAdminService
    {

        HttpResponseMessage? GetHttpResponseMessage();
        Task<HttpResponseMessage> CreateAdmin(CreateAdminRequest request);
    }
}
