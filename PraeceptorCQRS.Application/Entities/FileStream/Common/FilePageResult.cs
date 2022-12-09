namespace PraeceptorCQRS.Application.Entities.FileStream.Common
{
    public record FilePageResult(Domain.Entities.PageOf<Domain.Entities.SqlFileStream> Page);
}