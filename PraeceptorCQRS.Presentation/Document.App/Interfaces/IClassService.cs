using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.Class;

namespace Document.App.Interfaces;

public interface IClassService
{
    HttpResponseMessage? GetHttpResponseMessage();

    Task<int> GetClassCount(Guid instituteId);

    Task<HttpResponseMessage> PostPage(GetClassPageRequest request);

    Task<ClassModel?> GetClassById(Guid id);
    Task<ClassModel?> GetClassByCode(string code);
}