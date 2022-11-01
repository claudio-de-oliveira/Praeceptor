namespace PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType
{
    public record CreatePreceptorDegreeTypeRequest(
        string Code,
        Guid InstituteId
    );
}

