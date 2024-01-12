using System.Text;
using System.Text.Json;
using CustomerManagementService.Dtos;

namespace CustomerManagementService.Services.RequestDeleteCustomerSevices;

public class RequestDeleteCustomerService : IRequestDeleteCustomerService
{
    private readonly HttpClient _httpClient;

    public RequestDeleteCustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> RequestDeleteCustomerAsync(CustomerDto customer)
    {
        if(customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }
        try
        {
            var json = JsonSerializer.Serialize(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync($"customerManagement/requestCustomerDeletion/{customer.AuthId}", content);
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException(ex.Message);
        }
    }
}