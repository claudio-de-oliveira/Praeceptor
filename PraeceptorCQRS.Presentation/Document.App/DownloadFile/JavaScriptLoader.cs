using Ardalis.GuardClauses;

using System.Reflection;

namespace Document.App.Download;

// internal sealed class JavaScriptLoader
// {
//     public static JavaScriptLoader Instance { get; } = new JavaScriptLoader();
//
//     private const string Resource = "download.js";
//
//     public string JavaScript { get; private set; }
//
//     private JavaScriptLoader()
//     {
//         var assembly = typeof(JavaScriptLoader).GetTypeInfo().Assembly;
//
//         using var stream = assembly.GetManifestResourceStream(Resource);
//
//         Guard.Against.Null(stream);
//         using var reader = new StreamReader(stream);
//
//         JavaScript = reader.ReadToEnd();
//     }
// }