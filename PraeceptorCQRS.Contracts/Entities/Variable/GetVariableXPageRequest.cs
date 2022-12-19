namespace PraeceptorCQRS.Contracts.Entities.Variable
{
    public record GetVariableXPageRequest(
        Guid HoldingId,
        Guid InstituteId,
        Guid CourseId,
        int Curriculum,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? NameFilter,
        string? ValueFilter
        );
}
