namespace PraeceptorCQRS.Contracts.Entities.AxisType
{
    public record CreateAxisTypeRequest(
        string Code,
        string Code3,
        Guid InstituteId
    );
}