using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Extensions.Options;
namespace PhotoAnalyzer.Client.Services
{
    public class AzureAdOptions
    {
        public string? ClientId { get; set; }
        public string? TenantId { get; set; }
        public string? ClientSecret { get; set; }
        public string? RedirectUri { get; set; }
    }

    public class GraphServiceFactory
    {
        private readonly AzureAdOptions _options;

        public GraphServiceFactory(IOptions<AzureAdOptions> options)
        {
            _options = options.Value;
        }

        public GraphServiceClient CreateGraphServiceClient()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var credential = new AuthorizationCodeCredential(
                _options.TenantId,
                _options.ClientId,
                _options.ClientSecret,
                _options.RedirectUri
            );

            return new GraphServiceClient(credential, scopes);
        }
    }
}
