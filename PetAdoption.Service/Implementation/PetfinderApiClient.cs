using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetAdoption.Domain.DTO;
using PetAdoption.Domain;
using PetAdoption.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Service.Implementation
{
   public class PetfinderApiClient : IPetfinderApiClient
    {
        private readonly HttpClient _http;
        private readonly PetFinderOptions _options;
        private readonly ILogger<PetfinderApiClient> _log;
        private string? _token;
        private DateTime _tokenExpiresUtc;

        public PetfinderApiClient(HttpClient http, IOptions<PetFinderOptions> options,
                                  ILogger<PetfinderApiClient> log)
        {
            _http = http;
            _options = options.Value;
            _log = log;
        }

        private async Task EnsureTokenAsync()
        {
            if (_token != null && _tokenExpiresUtc > DateTime.UtcNow.AddMinutes(-5)) return;

            var form = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _options.ApiKey,
                ["client_secret"] = _options.ApiSecret
            });

            var res = await _http.PostAsync("/v2/oauth2/token", form);
            res.EnsureSuccessStatusCode();

            var tok = await res.Content.ReadFromJsonAsync<PetfinderTokenResponse>();
            _token = tok!.access_token;
            _tokenExpiresUtc = DateTime.UtcNow.AddSeconds(tok.expires_in);
            _http.DefaultRequestHeaders.Authorization = new("Bearer", _token);

            _log.LogInformation("Petfinder token refreshed (exp {exp})", _tokenExpiresUtc);
        }

        public async Task<IReadOnlyList<PetfinderAnimal>> SearchAnimalsAsync(string type, string zip, int limit = 10)
        {
            await EnsureTokenAsync();

            var url = $"/v2/animals?type={type}&location={zip}&limit={limit}";
            var json = await _http.GetFromJsonAsync<PetfinderAnimalSearchResponse>(url);
            return json!.animals;
        }
    }
}
