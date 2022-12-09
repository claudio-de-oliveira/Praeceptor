using Administrative.App.Models;
using Microsoft.AspNetCore.Mvc;
using PraeceptorCQRS.Contracts.Entities.SocialBody;

namespace Administrative.App.Interfaces;

public interface ISocialBodyService
{
    Task<HttpResponseMessage> CreateSocialBodyEntry([FromBody] CreateSocialBodyEntryRequest request);
    Task<int> GetSocialBodyEntriesCountByCourse(Guid courseId);
    Task<List<SocialBodyEntryModel>?> GetSocialBodyEntriesList(Guid courseId);
    Task<HttpResponseMessage> GetSocialBodyEntriesPage([FromBody] GetSocialBodyPageRequest request);
    Task<SocialBodyEntryModel?> GetSocialBodyEntry(Guid courseId, Guid preceptorId, Guid roleId);
    Task<HttpResponseMessage> DeleteSocialBodyEntry(Guid courseId, Guid preceptorId, Guid roleId);
    Task<HttpResponseMessage> DeleteSocialBodyByCourse(Guid courseId);
}
