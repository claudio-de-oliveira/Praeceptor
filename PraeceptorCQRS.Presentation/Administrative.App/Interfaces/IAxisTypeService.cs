using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.AxisType;

namespace Administrative.App.Interfaces
{
    public interface IAxisTypeService
    {
        Task<int> GetAxisTypeCount(Guid instituteId);
        Task<AxisTypeModel?> GetAxisTypeById(Guid id);
        Task<AxisTypeModel?> GetAxisTypeByCode(Guid instituteId, string code);
        Task<HttpResponseMessage> GetAxisTypePage(GetAxisTypePageRequest request);
        Task<HttpResponseMessage> UpdateAxisType(UpdateAxisTypeRequest request);
        Task<HttpResponseMessage> CreateAxisType(CreateAxisTypeRequest request);
        Task<HttpResponseMessage> DeleteAxisType(Guid id);
    }
}
