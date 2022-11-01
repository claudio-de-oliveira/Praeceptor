using Document.App.Interfaces;

namespace Document.App.Services
{
    public class SubSectionListService : EntityService, ISubSectionListService
    {
        // Just to inject the correct services
        public SubSectionListService(ConfigurationManager configuration)
            : base(configuration, "subsection", "subsubsection")
        {
            /* Nothing more todo */
        }
    }
}
