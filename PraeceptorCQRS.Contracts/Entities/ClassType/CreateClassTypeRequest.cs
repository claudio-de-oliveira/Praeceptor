namespace PraeceptorCQRS.Contracts.Entities.ClassType
{
    public record CreateClassTypeRequest(
        string Code,
        Guid InstituteId,
        bool IsRemote,
        int DurationInMinutes
    );
}