namespace PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType
{
    public record CreatePreceptorDegreeTypeRequest(
        string Code,
        bool LatoSensu,
        bool StrictoSensu,
        Guid InstituteId
    );
}