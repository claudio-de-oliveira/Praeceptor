
namespace PraeceptorCQRS.Contracts.Entities.Node
{
    public record CreateFirstNodeRequest(
        Guid FirstEntityId,
        Guid DocumentId,
        Guid SecondEntityId
        );
}
