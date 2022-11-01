using Document.App.Interfaces;

namespace Document.App.Services
{
    public class SubSubSectionListService : EntityService, ISubSubSectionListService
    {
        // Just to inject the correct services
        public SubSubSectionListService(ConfigurationManager configuration)
            : base(configuration, "subsubsection", "subsubsubsection")
        {
            /* Nothing more todo */
        }
    }
}
