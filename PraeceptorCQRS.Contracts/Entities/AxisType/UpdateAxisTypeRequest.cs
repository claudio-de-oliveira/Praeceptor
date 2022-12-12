namespace PraeceptorCQRS.Contracts.Entities.AxisType
{
    public record UpdateAxisTypeRequest(
        Guid Id,
        string Code,
        string Code3
    );
}
