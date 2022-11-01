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
        Task<HttpResponseMessage> GetFileStreamPage(GetFileStreamPageRequest request);
        Task<HttpResponseMessage> CreateFileStream(CreateSqlFileStreamRequest request);
        Task<HttpResponseMessage> DeleteFileStream(Guid id);
    }
}
