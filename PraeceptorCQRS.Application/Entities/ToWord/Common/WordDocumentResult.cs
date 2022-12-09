namespace PraeceptorCQRS.Application.Entities.ToWord.Common
{
    public record WordDocumentResult(
        MemoryStream Stream,
        string ContentType,
        string PathName
        );
}