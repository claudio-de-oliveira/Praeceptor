using IdentityModel.Client;

using Microsoft.AspNetCore.JsonPatch;

using Newtonsoft.Json;

using System.Net;

namespace PraeceptorCQRS.Utilities
{
    public class HttpAbstractService
    {
        protected readonly string _requestUri;
        protected readonly HttpClient _httpClient;

        private readonly JsonConverter jsonConverter;

        protected HttpResponseMessage? responseMessage { get; private set; }

        public readonly string? TokenEndpoint;

        protected HttpAbstractService(string uri, string identityServerURI, HttpClient httpClient, JsonConverter jsonConverter = null!)
        {
            _requestUri = uri;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_requestUri);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");

            var disco = _httpClient.GetDiscoveryDocumentAsync(identityServerURI).GetAwaiter().GetResult();

            if (string.IsNullOrEmpty(disco.Error))
                this.TokenEndpoint = disco.TokenEndpoint;

            this.jsonConverter = jsonConverter;
        }

        protected virtual async Task<string> GetAccessToken()
        {
            await Task.CompletedTask;
            return string.Empty;
        }

        public HttpResponseMessage? GetHttpResponseMessage() 
            => responseMessage;

        protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            try
            {
                var accessToken = await GetAccessToken();
                if (!string.IsNullOrWhiteSpace(accessToken))
                    _httpClient.SetBearerToken(accessToken);

                responseMessage = await _httpClient.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                responseMessage = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                };
            }

            return responseMessage;
        }

        private string GetUri(params object[] keys)
        {
            string uri = _requestUri;

            for (int p = 0; p < keys.Length; p++)
                uri += keys[p].ToString() + "/";
            return uri;
        }

        protected async Task<bool> Exist(params object[] keys)
        {
            string uri = GetUri(keys);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                return (jsonConverter is null)
                    ? await Task.FromResult(JsonConvert.DeserializeObject<bool>(responseBody))
                    : await Task.FromResult(JsonConvert.DeserializeObject<bool>(responseBody, jsonConverter));
            }

            return false;
        }

        protected async Task<int> Count(params object[] keys)
        {
            string uri = GetUri(keys);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                return (jsonConverter is null)
                    ? await Task.FromResult(JsonConvert.DeserializeObject<int>(responseBody))
                    : await Task.FromResult(JsonConvert.DeserializeObject<int>(responseBody, jsonConverter));
            }

            return -1;
        }

        protected async Task<List<T>?> GetMany<T>(params object[] keys) where T : class
        {
            string uri = GetUri(keys);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                return (jsonConverter is null)
                    ? await Task.FromResult(JsonConvert.DeserializeObject<List<T>>(responseBody))
                    : await Task.FromResult(JsonConvert.DeserializeObject<List<T>>(responseBody, jsonConverter));
            }

            return null;
        }

        protected async Task<T?> GetOne<T>(params object[] keys) where T : class
        {
            string uri = GetUri(keys);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                return (jsonConverter is null)
                    ? await Task.FromResult(JsonConvert.DeserializeObject<T>(responseBody))
                    : await Task.FromResult(JsonConvert.DeserializeObject<T>(responseBody, jsonConverter));
            }

            return null;
        }

        protected async Task<HttpResponseMessage> Create<T>(T obj, params object[] keys) where T : class
        {
            string uri = GetUri(keys);

            string serializedData = (jsonConverter is null)
                ? JsonConvert.SerializeObject(obj)
                : JsonConvert.SerializeObject(obj, jsonConverter);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(serializedData)
            };

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return await SendAsync(requestMessage);
        }

        protected async Task<HttpResponseMessage> Update<T>(T obj, params object[] keys) where T : class
        {
            string uri = GetUri(keys);

            string serializedData = (jsonConverter is null)
                ? JsonConvert.SerializeObject(obj)
                : JsonConvert.SerializeObject(obj, jsonConverter);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(serializedData)
            };

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await SendAsync(requestMessage);

            return await Task.FromResult(response);
        }

        protected async Task<HttpResponseMessage> Patch<T>(JsonPatchDocument<T> patchDoc, params object[] keys) where T : class
        {
            string uri = GetUri(keys);

            string serializedData = (jsonConverter is null)
                ? JsonConvert.SerializeObject(patchDoc)
                : JsonConvert.SerializeObject(patchDoc, jsonConverter);

            var requestMessage = new HttpRequestMessage(HttpMethod.Patch, uri)
            {
                Content = new StringContent(serializedData/*, Encoding.Unicode*/)
            };

            requestMessage.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await SendAsync(requestMessage);

            return await Task.FromResult(response);
        }

        protected async Task<HttpResponseMessage> Delete(params object[] keys)
        {
            string uri = GetUri(keys);

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            var response = await SendAsync(requestMessage);

            return await Task.FromResult(response);
        }

        ~HttpAbstractService()
        {
            _httpClient.DeleteAsync(_requestUri);
        }
    }
}

