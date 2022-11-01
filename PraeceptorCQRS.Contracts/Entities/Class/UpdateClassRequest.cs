namespace PraeceptorCQRS.Contracts.Entities.Class
{
    public record UpdateClassRequest(
        Guid Id,
        string Name,
        int Practice,
        int Theory,
        int PR,
        Guid TypeId
    );
}

