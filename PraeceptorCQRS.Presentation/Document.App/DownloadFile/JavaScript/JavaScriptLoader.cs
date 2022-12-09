using System.Reflection;

namespace Document.App.DownloadFile.JavaScript
{
    internal sealed class JavaScriptLoader
    {
        public static JavaScriptLoader Instance { get; } = new JavaScriptLoader();

        private const string Resource = "blazorDownloadFile.js";

        public string JavaScript { get; private set; }

        private JavaScriptLoader()
        {
            var assembly = typeof(JavaScriptLoader).GetTypeInfo().Assembly;

            using var stream = assembly.GetManifestResourceStream(Resource);
            using var reader = new StreamReader(stream);

            JavaScript = reader.ReadToEnd();
        }
    }
}