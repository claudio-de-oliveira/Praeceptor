using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.DocumentTemplate;
using PraeceptorCQRS.Contracts.Entities.SqlFileStream;

namespace Document.App.Interfaces
{
    public interface IFileStreamService
    {
        Task<int> GetFileStreamByInstituteCount(Guid instituteId);
        Task<FileModel?> GetFileStreamById(Guid id);
        Task<bool> ExistFileStream(Guid instituteId, string code);
        Task<FileModel?> GetFileStreamByCode(Guid instituteId, string code);
        Task<HttpResponseMessage> GetFileStreamPage(GetFilePageRequest request);
        Task<HttpResponseMessage> CreateFileStream(CreateFileRequest request);
        Task<HttpResponseMessage> DeleteFileStream(Guid id);
    }
}
