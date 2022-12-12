namespace PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType
{
    public record CreatePreceptorDegreeTypeRequest(
        string Code,
        string Code3,
        bool LatoSensu,
        bool StrictoSensu,
        Guid InstituteId
    );
}