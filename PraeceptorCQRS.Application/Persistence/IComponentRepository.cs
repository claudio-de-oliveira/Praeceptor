
using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Persistence
{
    public interface IComponentRepository
    {
        Task<IEnumerable<Component>> GetComponentListByCourseAndCurriculum(Guid courseId, int curriculum);
        Task<IEnumerable<Component>> GetComponentListByCourseAndCurriculumAndStage(Guid courseId, int curriculum, int season);
        Task<Component?> GetComponentByCourseAndCurriculumAndClass(Guid courseId, int curriculum, Guid classId);
        Task<IEnumerable<CurriculumResult>> GetCurriculumsByCourseId(Guid courseId);
        Task<Component?> CreateComponent(Component entityToCreate);
        Task<Component?> UpdateComponent(Component entityToUpdate);
        Task<Component?> DeleteComponent(Guid courseId, int curriculum, Guid classId);
    }
}
