using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.Class;

namespace Document.App.Interfaces;

public interface IClassService
{
    Task<int> GetClassCount(Guid instituteId);

    Task<HttpResponseMessage> PostPage(GetClassPageRequest request);

    Task<ClassModel?> GetClassByCode(string code);
}