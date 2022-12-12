using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.Class;

using System.Net;

namespace Document.App.Interfaces;

public interface IClassService
{
    HttpResponseMessage? GetHttpResponseMessage();

    Task<int> GetClassCount(Guid instituteId);

    Task<HttpResponseMessage> PostPage(GetClassPageRequest request);

    Task<ClassModel?> GetClassByCode(string code);
}