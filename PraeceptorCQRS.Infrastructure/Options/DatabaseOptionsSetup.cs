using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace PraeceptorCQRS.Infrastructure.Options
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private const string ConfigurationSectionName = "DatabaseOptions";

        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseOptions options)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            options.ConnectionString = connectionString;

            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }

    public class AuthenticationOptionsSetup : IConfigureOptions<AuthenticationOptions>
    {
        private const string ConfigurationSectionName = "AuthenticationOptions";

        private readonly IConfiguration _configuration;

        public AuthenticationOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(AuthenticationOptions options)
        {
            var connectionString = _configuration.GetConnectionString("IdentityServerHostConnection");

            options.ConnectionString = connectionString;

            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
