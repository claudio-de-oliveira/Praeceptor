namespace PraeceptorCQRS.Contracts.Entities.Node
{
    public record NodeResponse(
        Guid Id,
        Guid? PreviousNodeId,
        Guid? NextNodeId,
        Guid FirstEntityId,
        Guid SecondEntityId,
        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
    );
}