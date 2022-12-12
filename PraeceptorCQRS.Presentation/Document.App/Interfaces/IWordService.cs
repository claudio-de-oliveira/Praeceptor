using System.Net;

namespace Document.App.Interfaces;

public interface IWordService
{
    HttpResponseMessage? GetHttpResponseMessage();

    Task<Stream> ConvertPeaToDoc(Guid peaId);

    Task<Stream> ConvertPPPCToDoc(Guid docId);
}