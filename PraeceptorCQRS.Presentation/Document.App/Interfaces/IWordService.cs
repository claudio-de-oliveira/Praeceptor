namespace Document.App.Interfaces;

public interface IWordService
{
    Task<Stream> ConvertPeaToDoc(Guid peaId);

    Task<Stream> ConvertPPPCToDoc(Guid docId);
}