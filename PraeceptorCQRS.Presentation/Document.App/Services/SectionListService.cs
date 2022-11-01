using Document.App.Interfaces;

namespace Document.App.Services
{
    public class SectionListService : EntityService, ISectionListService
    {
        // Just to inject the correct services
        public SectionListService(ConfigurationManager configuration)
            : base(configuration, "section", "subsection")
        {
            /* Nothing more todo */
        }
    }
}
