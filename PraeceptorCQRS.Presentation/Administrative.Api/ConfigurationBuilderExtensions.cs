using Microsoft.Extensions.Configuration.Json;

namespace Administrative.Api
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddJsonFileAfterLastJsonFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            var jsonFileSource = new JsonConfigurationSource
            {
                FileProvider = null,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };
            jsonFileSource.ResolveFileProvider();
            var lastJsonFileSource = builder.Sources.LastOrDefault(s => s is JsonConfigurationSource);
            var indexOfLastJsonFileSource = builder.Sources.IndexOf(lastJsonFileSource);
            builder.Sources.Insert(indexOfLastJsonFileSource == -1
              ? builder.Sources.Count
              : indexOfLastJsonFileSource + 1, jsonFileSource);
            return builder;
        }
    }
}
