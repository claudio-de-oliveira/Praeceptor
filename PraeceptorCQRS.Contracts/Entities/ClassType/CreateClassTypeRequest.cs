namespace PraeceptorCQRS.Contracts.Entities.ClassType
{
    public record CreateClassTypeRequest(
        string Code,
        string Code3,
        Guid InstituteId,
        bool IsRemote,
        int DurationInMinutes
    );
}