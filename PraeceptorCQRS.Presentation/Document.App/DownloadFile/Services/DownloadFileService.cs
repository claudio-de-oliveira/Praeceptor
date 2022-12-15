using Document.App.DownloadFile.Interfaces;
using Document.App.DownloadFile.JavaScript;
using Document.App.DownloadFile.Utils;

using Microsoft.JSInterop;

namespace Document.App.DownloadFile.Services
{
    internal class DownloadFileService : IDownloadFileService
    {
        private readonly IJSRuntime _js;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFileService"/> class.
        /// </summary>
        /// <param name="js">The JSRuntime.</param>
        public DownloadFileService(IJSRuntime js)
        {
            _js = js;

            Task.Run(async () => await _js.InvokeVoidAsync("eval", JavaScriptLoader.Instance.JavaScript));
        }

        /// <see cref="IDownloadFileService.DownloadFileAsync(string, byte[])"/>
        public ValueTask<bool> DownloadFileAsync(string fileName, byte[] bytes)
        {
            // string s = Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
            return DownloadFileAsync(fileName, bytes, MimeTypeMap.GetMimeType(fileName));
        }

        public ValueTask<bool> DownloadFileAsync(string fileName, string content)
        {
            // string s = Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
            return DownloadFileAsync(fileName, content, MimeTypeMap.GetMimeType(fileName));
        }

        /// <see cref="IDownloadFileService.DownloadFileAsync(string, byte[], string)"/>
        public ValueTask<bool> DownloadFileAsync(string fileName, byte[] bytes, string contentType)
        {
#if NET5_0
            // Check if the IJSRuntime is the WebAssembly implementation of the JSRuntime
            if (_js is IJSUnmarshalledRuntime webAssemblyJSRuntime)
            {
                return ValueTask.FromResult(webAssemblyJSRuntime.InvokeUnmarshalled<string, string, byte[], bool>("BlazorDownloadFileFast", fileName, contentType, bytes));
            }

            // Fall back to the slow method if not in WebAssembly
            return BlazorDownloadFileAsync(fileName, bytes, contentType);
#else
            return BlazorDownloadFileAsync(fileName, bytes, contentType);
#endif
        }

        public ValueTask<bool> DownloadFileAsync(string fileName, string content, string contentType)
        {
#if NET5_0
            // Check if the IJSRuntime is the WebAssembly implementation of the JSRuntime
            if (_js is IJSUnmarshalledRuntime webAssemblyJSRuntime)
            {
                return ValueTask.FromResult(webAssemblyJSRuntime.InvokeUnmarshalled<string, string, byte[], bool>("BlazorDownloadFileFast", fileName, contentType, bytes));
            }

            // Fall back to the slow method if not in WebAssembly
            return BlazorDownloadFileAsync(fileName, bytes, contentType);
#else
            return BlazorDownloadFileAsync(fileName, content, contentType);
#endif
        }

        private ValueTask<bool> BlazorDownloadFileAsync(string fileName, byte[] bytes, string contentType)
        {
            return _js.InvokeAsync<bool>("blazorDownloadFile", fileName, contentType, bytes);
        }

        private ValueTask<bool> BlazorDownloadFileAsync(string fileName, string content, string contentType)
        {
            return _js.InvokeAsync<bool>("blazorDownloadFile", fileName, content, contentType);
        }
    }
}