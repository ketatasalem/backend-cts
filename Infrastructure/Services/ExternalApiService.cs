using Labo_Cts_backend.Shared.Models;
using Microsoft.Extensions.Options;

namespace Labo_Cts_backend.Infrastructure.Services
{
    public class ExternalApiService(HttpClient httpClient, IOptions<ExternalApiOptions> options)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ExternalApiOptions _options = (ExternalApiOptions)options;

        public async Task<string> GetDataAsync()
        {
            var response = await _httpClient.GetAsync($"{_options.BaseUrl}/endpoint?api_key={_options.ApiKey}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
