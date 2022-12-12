using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.SqlDocxStream;
using PraeceptorCQRS.Contracts.Entities.ToWord;

using System.Net;

namespace Document.App.Interfaces
{
    public interface IDocxStreamService
    {
        HttpResponseMessage? GetHttpResponseMessage();

        Task<int> GetDocxStreamByInstituteCount(Guid instituteId);

        Task<DocxModel?> GetDocxStreamById(Guid id);

        Task<bool> ExistDocxStream(Guid instituteId, string code);

        Task<DocxModel?> GetDocxStreamByCode(Guid instituteId, string code);

        Task<HttpResponseMessage> GetDocxStreamPage(GetDocxPageRequest request);

        // Task<HttpResponseMessage> CreateDocxStream(CreateDocxRequest request);
        Task<HttpResponseMessage> CreateDocxStream(ConvertPpcToDocxRequest request);

        Task<HttpResponseMessage> DeleteDocxStream(Guid id);
    }
}