using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.Pea;

namespace Document.App.Interfaces;

public interface IPlannerService
{
    Task<PlannerModel?> GetPlannerFromId(Guid id);

    Task<List<PlannerModel>?> GetPlannerFromClassId(Guid classId);

    Task<HttpResponseMessage> GetPlannerPage(GetPeaPageRequest request);

    Task<HttpResponseMessage> CreatePlanner(CreatePeaRequest request);

    Task<HttpResponseMessage> UpdatePlanner(UpdatePeaRequest request);

    Task<HttpResponseMessage> DeletePlanner(Guid classId);
}