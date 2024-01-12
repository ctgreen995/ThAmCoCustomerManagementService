namespace ThAmCoCustomerApiGateway.Services.Auth0Token;

public interface IAuth0TokenService
{
    Task<string> GetTokenAsync(string clientId, string clientSecret, string audience);
}