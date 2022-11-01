using Document.App.Interfaces;

namespace Document.App.Services
{
    public class DocumentListService : EntityService, IDocumentListService
    {
        // Just to inject the correct services
        public DocumentListService(ConfigurationManager configuration)
            : base(configuration, "document", "chapter")
        {
            /* Nothing more todo */
        }
    }
}
