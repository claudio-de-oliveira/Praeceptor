using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType;

using System.Net;

namespace Administrative.App.Interfaces;

public interface IPreceptorRegimeService
{
    HttpResponseMessage? GetHttpResponseMessage();

    Task<int> GetPreceptorRegimeTypeCount(Guid instituteId);
    Task<HttpResponseMessage> PostPage(GetPreceptorRegimeTypePageRequest request);
    Task<PreceptorRegimeTypeModel?> GetPreceptorRegimeTypeById(Guid id);
    Task<PreceptorRegimeTypeModel?> GetPreceptorRegimeTypeByCode(Guid instituteId, string code);
    Task<HttpResponseMessage> UpdatePreceptorRegimeType(UpdatePreceptorRegimeTypeRequest request);
    Task<HttpResponseMessage> CreatePreceptorRegimeType(CreatePreceptorRegimeTypeRequest request);
    Task<HttpResponseMessage> DeletePreceptorRegimeType(Guid id);
}
