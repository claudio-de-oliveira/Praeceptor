using Document.App.Models;

using System.Net;

namespace Document.App.Interfaces
{
    public interface IInstituteService
    {
        HttpResponseMessage? GetHttpResponseMessage();
        Task<InstituteModel?> GetInstituteById(Guid id);
    }
}
