﻿using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.Preceptor;

namespace Administrative.App.Interfaces
{
    public interface IPreceptorService
    {
        Task<int> GetPreceptorCount(Guid instituteId);
        Task<HttpResponseMessage> PostPage(GetPreceptorPageRequest request);
        Task<PreceptorModel?> GetPreceptorById(Guid id);
        Task<PreceptorModel?> GetPreceptorByCode(string code);
        Task<HttpResponseMessage> UpdatePreceptor(UpdatePreceptorRequest request);
        Task<HttpResponseMessage> CreatePreceptor(CreatePreceptorRequest request);
        Task<HttpResponseMessage> DeletePreceptor(Guid id);
    }
}
