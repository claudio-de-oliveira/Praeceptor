using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;

using System.Net;

namespace Administrative.App.Interfaces;

public interface IPreceptorRoleService
{
    HttpResponseMessage? GetHttpResponseMessage();

    Task<int> GetPreceptorRoleTypeCount(Guid instituteId);
    Task<HttpResponseMessage> PostPage(GetPreceptorRoleTypePageRequest request);
    Task<PreceptorRoleTypeModel?> GetPreceptorRoleTypeById(Guid id);
    Task<PreceptorRoleTypeModel?> GetPreceptorRoleTypeByCode(Guid instituteId, string code);
    Task<HttpResponseMessage> UpdatePreceptorRoleType(UpdatePreceptorRoleTypeRequest request);
    Task<HttpResponseMessage> CreatePreceptorRoleType(CreatePreceptorRoleTypeRequest request);
    Task<HttpResponseMessage> DeletePreceptorRoleType(Guid id);
}
