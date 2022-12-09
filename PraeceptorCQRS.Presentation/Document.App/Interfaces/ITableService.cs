using PraeceptorCQRS.Contracts.Entities.Class;
using PraeceptorCQRS.Contracts.Entities.SimpleTable;

namespace Document.App.Interfaces;

public interface ITableService
{
    Task<int> GetTablesCount(Guid instituteId);

    Task<HttpResponseMessage> PostPage(GetSimpleTablePageRequest request);
}