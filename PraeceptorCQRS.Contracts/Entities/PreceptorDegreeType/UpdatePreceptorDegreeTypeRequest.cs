namespace PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType
{
    public record UpdatePreceptorDegreeTypeRequest(
        Guid Id,
        string Code,
        string Code3,
        bool LatoSensu,
        bool StrictoSensu
    );
}

