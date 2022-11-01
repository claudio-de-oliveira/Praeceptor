using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.Group;

namespace Document.App.Interfaces
{
    public interface IGroupService
    {
        Task<bool> Exists(Guid instituteId, string code);
        Task<HttpResponseMessage> CreateGroup(CreateGroupRequest request);
        Task<GroupModel?> GetGroupById(Guid id);
        Task<GroupModel?> GetGroupByCode(Guid instituteId, string code);
        Task<int> GetGroupsCountByInstitute(Guid instituteId);
        Task<HttpResponseMessage> GetGroupPage(GetGroupPageRequest request);
        Task<HttpResponseMessage> DeleteGroup(Guid id);
    }
}
