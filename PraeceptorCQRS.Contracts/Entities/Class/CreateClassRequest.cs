namespace PraeceptorCQRS.Contracts.Entities.Class
{
    public record CreateClassRequest(
        string Code,
        string Name,
        int Practice,
        int Theory,
        int PR,
        Guid InstituteId,
        Guid TypeId
    );
}

