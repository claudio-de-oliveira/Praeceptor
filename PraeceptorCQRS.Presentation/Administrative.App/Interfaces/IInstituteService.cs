using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.Institute;

namespace Administrative.App.Interfaces;

public interface IInstituteService
{
    Task<int> GetInstituteCount(Guid holdingId);
    Task<HttpResponseMessage> PostPage(GetInstitutePageRequest request);
    Task<InstituteModel?> GetInstituteById(Guid id);
    Task<InstituteModel?> GetInstituteByCode(string code);
    Task<HttpResponseMessage> UpdateInstitute(UpdateInstituteRequest request);
    Task<HttpResponseMessage> CreateInstitute(CreateInstituteRequest request);
    Task<HttpResponseMessage> DeleteInstitute(Guid id);
}
