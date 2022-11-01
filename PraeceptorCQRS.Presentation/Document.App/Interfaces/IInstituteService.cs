using Document.App.Models;

namespace Document.App.Interfaces
{
    public interface IInstituteService
    {
        Task<InstituteModel?> GetInstituteById(Guid id);
    }
}
