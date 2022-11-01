﻿using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.VariableValue;

namespace Document.App.Interfaces
{
    public interface IVariableValueService
    {
        Task<HttpResponseMessage> CreateVariableValue(CreateVariableValueRequest request);
        Task<VariableValueModel?> GetVariableValueById(Guid id);
        Task<VariableValueModel?> GetVariableValueByVariableAndGroupValue(Guid groupValueId, Guid variableId);
        Task<HttpResponseMessage> UpdateVariableValue(UpdateVariableValueRequest request);
        Task<HttpResponseMessage> DeleteVariableValuesFromVariable(Guid variableId);
        Task<HttpResponseMessage> DeleteVariableValue(Guid id);
    }
}