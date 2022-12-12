using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.ClassType;

using System.Net;

namespace Administrative.App.Interfaces;

public interface IClassTypeService
{
    HttpResponseMessage? GetHttpResponseMessage();

    Task<int> GetClassTypeCount(Guid instituteId);

    Task<HttpResponseMessage> PostPage(GetClassTypePageRequest request);

    Task<ClassTypeModel?> GetClassTypeById(Guid id);

    Task<ClassTypeModel?> GetClassTypeByCode(Guid instituteId, string code);

    Task<HttpResponseMessage> UpdateClassType(UpdateClassTypeRequest request);

    Task<HttpResponseMessage> CreateClassType(CreateClassTypeRequest request);

    Task<HttpResponseMessage> DeleteClassType(Guid id);
}