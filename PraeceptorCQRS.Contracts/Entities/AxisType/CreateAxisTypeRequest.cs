namespace PraeceptorCQRS.Contracts.Entities.AxisType
{
    public record CreateAxisTypeRequest(
        string Code,
        Guid InstituteId
    );
}
