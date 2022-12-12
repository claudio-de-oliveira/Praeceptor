using PraeceptorCQRS.Contracts.Entities.ClassType;

using System.Net;

namespace Document.App.Interfaces;

public interface IClassTypeService
{
    HttpResponseMessage? GetHttpResponseMessage();

    Task<int> GetClassTypeCount(Guid instituteId);

    Task<HttpResponseMessage> PostPage(GetClassTypePageRequest request);
}