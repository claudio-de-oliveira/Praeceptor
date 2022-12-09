namespace PraeceptorCQRS.Contracts.Entities.ToWord
{
    public record ConvertPeaToDocxRequest(
        Guid PeaId,
        string CourseName,
        int Season
        );
}