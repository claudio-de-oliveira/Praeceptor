﻿using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.GroupValue;

namespace Document.App.Interfaces
{
    public interface IGroupValueService
    {
        Task<HttpResponseMessage> CreateGroupValue(CreateGroupValueRequest request);
        Task<GroupValueModel?> GetGroupValueById(Guid id);
        Task<List<GroupValueModel>?> GetGroupValuesByGroup(Guid groupId);
        Task<HttpResponseMessage> UpdateGroupValue(UpdateGroupValueRequest request);
        Task<HttpResponseMessage> DeleteGroupValue(Guid id);
        Task<HttpResponseMessage> DeleteGroupValuesFromGroup(Guid groupId);
    }
}