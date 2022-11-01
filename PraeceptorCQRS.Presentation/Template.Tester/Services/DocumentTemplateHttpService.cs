using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.DocumentTemplate;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Template.Tester.Services
{
    internal class DocumentTemplateHttpService : HttpService
    {
        public DocumentTemplateHttpService(
            string uri,
            string identityServerURI,
            HttpClient httpClient,
            JsonConverter jsonConverter = null!
            )
            : base(uri, identityServerURI, httpClient, jsonConverter)
        {
        }

        protected override async Task<string> GetAccessToken()
        {
            await Task.CompletedTask;
            return string.Empty;
        }

        public async Task<DocumentTemplateResponse?> GetDocumentTemplate(Guid id)
            => await base.GetOne<DocumentTemplateResponse>("documenttemplate", "get", "id", id);
        public async Task<HttpResponseMessage> CreateDocumentTemplate(CreateDocumentTemplateRequest request)
            => await base.Create<CreateDocumentTemplateRequest>(request, "documenttemplate", "create");
        public async Task<HttpResponseMessage> UpdateDocumentTemplate(UpdateDocumentTemplateRequest request)
            => await base.Update(request, "documenttemplate", "update");
        public async Task<HttpResponseMessage> DeleteDocumentTemplate(Guid id)
            => await base.Delete("documenttemplate", "delete", id);
    }
}

