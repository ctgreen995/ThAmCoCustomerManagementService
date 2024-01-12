using Newtonsoft.Json;

namespace ThAmCoCustomerApiGateway.Services.Auth0Token;

public class Auth0TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}