namespace PraeceptorCQRS.Contracts.Entities.ClassType
{
    public record UpdateClassTypeRequest(
        Guid Id,
        bool IsRemote,
        int DurationInMinutes
    );
}