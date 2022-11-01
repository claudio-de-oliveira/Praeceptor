using Document.App.Models;
using Document.App.Requests;

using PraeceptorCQRS.Contracts.Entities.Node;

namespace Document.App.Interfaces
{
    public interface IEntityService
    {
        Task<int> GetEntitiesCount(Guid instituteId);
        Task<List<BookEntity>?> GetAllEntities(Guid parentId);
        Task<List<BookEntity>?> GetEntitiesPage(Guid parentId, int start, int count);
        Task<BookEntity?> GetEntity(Guid id);
        Task<List<BookEntity>?> GetEntityList(Guid id);

        Task<HttpResponseMessage> PostPage(GetEntityPageRequest request);
        Task<HttpResponseMessage> CreateEntity(CreateEntityRequest request);
        Task<HttpResponseMessage> UpdateEntity(UpdateEntityRequest request);
        Task<HttpResponseMessage> DeleteEntity(Guid id);

        Task<HttpResponseMessage> CreateFirstEntity(CreateFirstNodeRequest request);
        Task<HttpResponseMessage> InsertEntityAfterPosition(InsertNodeRequest request);
        Task<HttpResponseMessage> InsertEntityBeforePosition(InsertNodeRequest request);

        Task<bool> MoveEntityForward(Guid parent, Guid position);
        Task<bool> MoveEntityBackward(Guid parent, Guid position);

        Task<NodeResponse?> GetFirstEntityPosition(Guid documentId, Guid id);
        Task<NodeResponse?> GetNextEntityPosition(Guid id);
        Task<NodeResponse?> GetLastEntityPosition(Guid documentId, Guid id);
        Task<NodeResponse?> GetPreviousEntityPosition(Guid id);

        Task<NodeResponse?> GetEntityPosition(Guid firstEntityId, Guid documentId, Guid secondEntityId);

        Task<HttpResponseMessage> DeleteEntityAtPosition(Guid position);
    }
}
