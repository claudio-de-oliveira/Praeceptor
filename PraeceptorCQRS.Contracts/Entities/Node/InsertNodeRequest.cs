
namespace PraeceptorCQRS.Contracts.Entities.Node
{
    public record InsertNodeRequest(
        Guid Id,
        Guid FirstEntityId,
        Guid DocumentId,
        Guid SecondEntityId
        );
}
