using Document.App.Interfaces;

using PraeceptorCQRS.Contracts.Entities.SimpleTable;

namespace Document.App.Services;

public class TableService : ITableService
{
    public Task<int> GetTablesCount(Guid instituteId)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> PostPage(GetSimpleTablePageRequest request)
    {
        throw new NotImplementedException();
    }
}