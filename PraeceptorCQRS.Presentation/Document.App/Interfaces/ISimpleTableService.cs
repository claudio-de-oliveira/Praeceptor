using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.SimpleTable;

namespace Document.App.Interfaces
{
    public interface ISimpleTableService
    {
        Task<int> GetTablesCountByInstitute(Guid instituteId);

        Task<HttpResponseMessage> GetTablePage(GetSimpleTablePageRequest request);

        Task<SimpleTableModel?> GetTableById(Guid id);

        Task<SimpleTableModel?> GetTableByCode(string code, Guid instituteId);

        Task<HttpResponseMessage> CreateTable(CreateSimpleTableRequest request);

        Task<HttpResponseMessage> UpdateTable(UpdateSimpleTableRequest request);

        Task<HttpResponseMessage> DeleteTable(Guid id);
    }
}