using Document.App.Interfaces;

namespace Document.App.Services
{
    public class ChapterListService : EntityService, IChapterListService
    {
        // Just to inject the correct services
        public ChapterListService(ConfigurationManager configuration)
            : base(configuration, "chapter", "section")
        {
            /* Nothing more todo */
        }
    }
}
