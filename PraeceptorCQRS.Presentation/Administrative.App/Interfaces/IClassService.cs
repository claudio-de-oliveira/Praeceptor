using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.Class;

using System.Net;

namespace Administrative.App.Interfaces;

public interface IClassService
{
    HttpResponseMessage? GetHttpResponseMessage();

    Task<int> GetClassCount(Guid instituteId);

    Task<HttpResponseMessage> PostPage(GetClassPageRequest request);

    Task<ClassModel?> GetClassById(Guid id);

    Task<ClassModel?> GetClassByCode(string code);

    Task<HttpResponseMessage> UpdateClass(UpdateClassRequest request);

    Task<HttpResponseMessage> CreateClass(CreateClassRequest request);

    Task<HttpResponseMessage> DeleteClass(Guid id);
}