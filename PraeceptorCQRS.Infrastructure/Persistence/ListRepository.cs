using Ardalis.GuardClauses;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class ListRepository : AbstractRepository<Node>, IListRepository
    {
        public ListRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<bool> Exists(Func<Node, bool> predicate)
            => await ReadDefault(predicate) is not null;

        public async Task<Node?> GetAt(Guid? id)
            => await ReadDefault(o => o.Id == id);

        public async Task<Node?> GetFirstPosition(Guid firstEntityId)
            => await ReadDefault(o => o.FirstEntityId == firstEntityId && o.PreviousNodeId is null);
        public async Task<Node?> GetLastPosition(Guid firstEntityId)
            => await ReadDefault(o => o.FirstEntityId == firstEntityId && o.NextNodeId is null);

        public async Task<Node?> CreateFirst(Node node)
            => await CreateDefault(node);

        public async Task MoveBackward(Node node)
        {
            var previous = await ReadDefault(o => o.Id == node.PreviousNodeId);
            Guard.Against.Null(previous);

            var tmp = previous.SecondEntityId;
            previous.SecondEntityId = node.SecondEntityId;
            node.SecondEntityId = tmp;

            DetachLocal(o => o.Id == previous.Id);
            previous.LastModified = DateTime.UtcNow;
            // previous.LastModifiedBy = lastModifiedBy;
            previous = await UpdateDefault(previous);
            Guard.Against.Null(previous);

            // NÃO PRECISA ATUALIZAR O NÓ. POR QUÊ?
            // _repository.DetachLocal(o => o.Id == node.Id);
            // node.LastModified = DateTime.UtcNow;
            // // node.LastModifiedBy = lastModifiedBy;
            // node = await _repository.UpdateDefault(node);
            // Guard.Against.Null(node);
        }

        public async Task MoveForward(Node node)
        {
            var next = await ReadDefault(o => o.Id == node.NextNodeId);
            Guard.Against.Null(next);

            var tmp = next.SecondEntityId;
            next.SecondEntityId = node.SecondEntityId;
            node.SecondEntityId = tmp;

            DetachLocal(o => o.Id == next.Id);
            next.LastModified = DateTime.UtcNow;
            // next.LastModifiedBy = lastModifiedBy;
            next = await UpdateDefault(next);
            Guard.Against.Null(next);

            // NÃO PRECISA ATUALIZAR O NÓ. POR QUÊ?
            // _repository.DetachLocal(o => o.Id == node.Id);
            // node.LastModified = DateTime.UtcNow;
            // // node.LastModifiedBy = lastModifiedBy;
            // node = await _repository.UpdateDefault(node);
            // Guard.Against.Null(node);
        }

        public async Task<Node?> InsertAfter(Guid id, Node node)
        {
            Node? previous = await ReadDefault(o => o.Id == id);
            if (previous is null)
                return null;

            Node? next = await ReadDefault(o => o.Id == previous.NextNodeId);

            node.PreviousNodeId = previous.Id;
            node.NextNodeId = next?.Id;

            var created = await CreateDefault(node);
            if (created is null)
                return null;

            previous.NextNodeId = created.Id;
            _ = await UpdateDefault(previous);

            if (next is not null)
            {
                next.PreviousNodeId = created.Id;
                _ = await UpdateDefault(next);
            }

            return created;
        }

        public async Task<Node?> InsertBefore(Guid id, Node node)
        {
            Node? next = await ReadDefault(o => o.Id == id);
            if (next is null)
                return null;

            Node? previous = await ReadDefault(o => o.Id == next.PreviousNodeId);

            node.PreviousNodeId = previous?.Id;
            node.NextNodeId = next.Id;

            var created = await CreateDefault(node);
            if (created is null)
                return null;

            next.PreviousNodeId = created.Id;
            _ = await UpdateDefault(next);

            if (previous is not null)
            {
                previous.NextNodeId = created.Id;
                _ = await UpdateDefault(previous);
            }

            return created;
        }

        public async Task<Node?> UpdateNode(Node node)
        {
            DetachLocal(o => o.Id == node.Id);

            var updated = await UpdateDefault(node);

            return updated;
        }

        public async Task Remove(Guid id)
        {
            var node = await ReadDefault(o => o.Id == id);

            if (node is null)
                return;

            if (node.PreviousNodeId is not null)
            {
                var previous = await ReadDefault(o => o.Id == node.PreviousNodeId);
                Guard.Against.Null(previous);
                previous.NextNodeId = node.NextNodeId;
                await UpdateDefault(previous);
            }

            if (node.NextNodeId is not null)
            {
                var next = await ReadDefault(o => o.Id == node.NextNodeId);
                Guard.Against.Null(next);
                next.PreviousNodeId = node.PreviousNodeId;
                await UpdateDefault(next);
            }

            _ = await DeleteDefault(node);
        }
    }
}
