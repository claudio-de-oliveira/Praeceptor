﻿using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.Component;

namespace Administrative.App.Interfaces
{
    public interface IComponentService
    {
        Task<HttpResponseMessage> CreateComponent(CreateComponentRequest request);
        Task<HttpResponseMessage> UpdateComponent(UpdateComponentRequest request);
        Task<ComponentModel?> GetComponentByCourseAndCurriculumAndClass(Guid courseId, int curriculum, Guid classId);
        Task<IEnumerable<ComponentModel>?> GetComponentListByCourseAndCurriculum(Guid courseId, int curriculum);
        Task<IEnumerable<ComponentModel>?> GetComponentListByCourseAndCurriculumAndSeason(Guid courseId, int curriculum, int season);
        Task<List<CurriculumModel>?> GetCurriculumsByCourseId(Guid courseId);
        Task<HttpResponseMessage> DeleteComponent(Guid courseId, int curriculum, Guid classId);
    }
}