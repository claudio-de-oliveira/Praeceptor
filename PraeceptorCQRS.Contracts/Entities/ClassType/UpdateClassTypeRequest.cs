namespace PraeceptorCQRS.Contracts.Entities.ClassType
{
    public record UpdateClassTypeRequest(
        Guid Id,
        string Code,
        string Code3,
        bool IsRemote,
        int DurationInMinutes
    );
}