using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Persistence
{
    public interface IListRepository
    {
        Task<List<Node>> QueryDefault(string sql);
        Task<bool> Exists(Func<Node, bool> predicate);
        Task<Node?> GetAt(Guid? id);

        Task<Node?> GetFirstPosition(Guid firstEntityId);
        Task<Node?> GetLastPosition(Guid firstEntityId);

        Task<Node?> CreateFirst(Node relationship);

        Task MoveBackward(Node position);
        Task MoveForward(Node position);

        Task<Node?> InsertAfter(Guid id, Node relationship);
        Task<Node?> InsertBefore(Guid id, Node relationship);

        Task<Node?> UpdateNode(Node relationship);

        Task Remove(Guid id);
    }
}
