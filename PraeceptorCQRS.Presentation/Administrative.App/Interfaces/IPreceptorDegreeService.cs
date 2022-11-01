using Administrative.App.Models;

using PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType;

namespace Administrative.App.Interfaces
{
    public interface IPreceptorDegreeService
    {
        Task<int> GetPreceptorDegreeTypeCount(Guid instituteId);
        Task<HttpResponseMessage> PostPage(GetPreceptorDegreeTypePageRequest request);
        Task<PreceptorDegreeTypeModel?> GetPreceptorDegreeTypeById(Guid id);
        Task<PreceptorDegreeTypeModel?> GetPreceptorDegreeTypeByCode(Guid instituteId, string code);
        Task<HttpResponseMessage> UpdatePreceptorDegreeType(UpdatePreceptorDegreeTypeRequest request);
        Task<HttpResponseMessage> CreatePreceptorDegreeType(CreatePreceptorDegreeTypeRequest request);
        Task<HttpResponseMessage> DeletePreceptorDegreeType(Guid id);
    }
}
