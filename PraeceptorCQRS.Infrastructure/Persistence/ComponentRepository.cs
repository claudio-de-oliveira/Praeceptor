using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;

using System.Linq;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class ComponentRepository : AbstractRepository<Component>, IComponentRepository
    {

        public ComponentRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<IEnumerable<Component>> GetComponentListByCourseAndCurriculum(Guid courseId, int curriculum)
            => await ListDefault(o => o.CourseId == courseId && o.Curriculum == curriculum);
        public async Task<IEnumerable<Component>> GetComponentListByCourseAndCurriculumAndStage(Guid courseId, int curriculum, int season)
            => await ListDefault(o => o.CourseId == courseId && o.Curriculum == curriculum && o.Season == season);
        public async Task<Component?> GetComponentByCourseAndCurriculumAndClass(Guid courseId, int curriculum, Guid classId)
            => await ReadDefault(o => o.CourseId == courseId && o.Curriculum == curriculum && o.ClassId == classId);
        public async Task<Component?> CreateComponent(Component entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<Component?> UpdateComponent(Component entityToUpdate)
        {
            DetachLocal(o => o.CourseId == entityToUpdate.CourseId && o.Curriculum == entityToUpdate.Curriculum && o.Season == entityToUpdate.Season && o.ClassId == entityToUpdate.ClassId);
            return await UpdateDefault(entityToUpdate);
        }
        public async Task<IEnumerable<CurriculumResult>> GetCurriculumsByCourseId(Guid courseId)
        {
            var list = await base.ListDefault(o => o.CourseId == courseId);
            return list.Select(o => new CurriculumResult(o.Curriculum)).Distinct().ToList();
        }
        public async Task<Component?> DeleteComponent(Guid courseId, int curriculum, Guid classId)
        {
            var entityToDelete = await ReadDefault(o => o.CourseId == courseId && o.Curriculum == curriculum && o.ClassId == classId);

            if (entityToDelete is not null)
                return await DeleteDefault(entityToDelete);

            return null;
        }
    }
}
