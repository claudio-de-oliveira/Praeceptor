using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.Class;
using PraeceptorCQRS.Contracts.Entities.Variable;

namespace Document.App.Interfaces
{
    public interface IVariableXService
    {
        Task<HttpResponseMessage> CreateVariableByHolding(CreateVariableXRequest request);
        Task<HttpResponseMessage> CreateVariableByInstitute(CreateVariableXRequest request);
        Task<HttpResponseMessage> CreateVariableByCourse(CreateVariableXRequest request);
        Task<HttpResponseMessage> UpdateVariableByHolding(UpdateVariableXRequest request);
        Task<HttpResponseMessage> UpdateVariableByInstitute(UpdateVariableXRequest request);
        Task<HttpResponseMessage> UpdateVariableByCourse(UpdateVariableXRequest request);
        Task<HttpResponseMessage> DeleteVariable(Guid id);
        Task<List<VariableXModel>?> GetVariablesByHolding(Guid holdingId);
        Task<List<VariableXModel>?> GetVariablesByInstitute(Guid instituteId);
        Task<List<VariableXModel>?> GetVariablesByCourseAndCurriculum(Guid courseId, int curriculum);
        Task<HttpResponseMessage> PostPage(GetVariableXPageRequest request);
    }
}
