namespace PraeceptorCQRS.Contracts.Entities.Component
{
    public record CreateComponentRequest(
        Guid CourseId,
        int Curriculum,
        int Season,
        Guid ClassId,
        Guid AxisTypeId,
        bool Optative
        );
}
