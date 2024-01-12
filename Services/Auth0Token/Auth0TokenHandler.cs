using System.Net.Http.Headers;

namespace ThAmCoCustomerApiGateway.Services.Auth0Token;

public class Auth0TokenHandler : DelegatingHandler
{
    private readonly IAuth0TokenService _tokenService;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _audience;

    public Auth0TokenHandler(IAuth0TokenService tokenService, string clientId, string clientSecret, string audience)
    {
        _tokenService = tokenService;
        _clientId = clientId;
        _clientSecret = clientSecret;
        _audience = audience;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await _tokenService.GetTokenAsync(_clientId, _clientSecret, _audience);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}