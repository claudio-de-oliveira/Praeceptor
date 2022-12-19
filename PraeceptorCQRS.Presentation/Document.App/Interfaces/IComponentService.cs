using Document.App.Models;

namespace Document.App.Interfaces
{
    public interface IComponentService
    {
        Task<List<CurriculumModel>?> GetCurriculaByCourseId(Guid courseId);
    }
}
