namespace PraeceptorCQRS.Application.Persistence
{
    public interface IPeaRepository
    {
        Task<Domain.Entities.Pea?> CreatePea(Domain.Entities.Pea entityToCreate);

        Task<bool> Exists(Func<Domain.Entities.Pea, bool> predicate);

        Task<Domain.Entities.Pea?> GetPeaById(Guid id);

        Task<Domain.Entities.Pea?> GetPeaByClassId(Guid classId);

        Task UpdatePea(Domain.Entities.Pea entityToUpdate);

        Task DeletePea(Guid id);

        Task<Domain.Entities.PageOf<Domain.Entities.Pea>> GetPeaPage(
            Guid classId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            );
    }
}