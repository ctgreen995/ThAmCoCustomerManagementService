using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ThAmCoCustomerApiGateway.Services.Auth0Token;

public class Auth0TokenService : IAuth0TokenService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private IConfiguration _configuration;

    public Auth0TokenService(HttpClient httpClient, IMemoryCache cache, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _cache = cache;
        _configuration = configuration;
    }

    public async Task<string> GetTokenAsync(string clientId, string clientSecret, string audience)
    {
        var cacheKey = $"token_{audience}";

        // Try to get the token from the cache
        if (_cache.TryGetValue(cacheKey, out string token))
        {
            return token;
        }

        string tokenUrl = $"https://{_configuration["Auth0Authority"]}/oauth/token";
        var tokenResponse = await _httpClient.PostAsync(tokenUrl,
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "audience", audience }
            }));

        tokenResponse.EnsureSuccessStatusCode();
        var responseContent = await tokenResponse.Content.ReadAsStringAsync();
        var tokenObject = JsonConvert.DeserializeObject<Auth0TokenResponse>(responseContent);

        // Cache the new token
        var options =
            new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)); // Set cache duration as needed
        _cache.Set(cacheKey, tokenObject.AccessToken, options);

        return tokenObject.AccessToken;
    }
}