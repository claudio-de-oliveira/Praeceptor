namespace PraeceptorCQRS.Application.Persistence
{
    public interface ICourseRepository
    {
        Task<int> GetCoursesCountByInstitute(Guid instituteId);
        Task<Domain.Entities.PageOf<Domain.Entities.Course>> GetCoursePage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? codeFilter,
            string? nameFilter,
            int? AC,
            int? NumberOfSeasons,
            int? MinimumWorkload,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            );
        Task<List<Domain.Entities.Course>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.Course, bool> predicate);
        Task<Domain.Entities.Course?> GetCourseById(Guid id);
        Task<Domain.Entities.Course?> GetCourseByCode(string code);
        Task<Domain.Entities.Course?> CreateCourse(Domain.Entities.Course entityToCreate);
        Task UpdateCourse(Domain.Entities.Course entityToUpdate);
        Task DeleteCourse(Guid id);
    }
}

