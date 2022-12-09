using PraeceptorCQRS.Contracts.Entities.ClassType;

namespace Document.App.Interfaces;

public interface IClassTypeService
{
    Task<int> GetClassTypeCount(Guid instituteId);

    Task<HttpResponseMessage> PostPage(GetClassTypePageRequest request);
}