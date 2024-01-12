using CustomerManagementService.Dtos;

namespace CustomerManagementService.Services.RequestDeleteCustomerSevices
{
    public class FakeRequestDeleteCustomerService : IRequestDeleteCustomerService
    {
        public Task<HttpResponseMessage> RequestDeleteCustomerAsync(CustomerDto customer)
        {
            // Check for null customer
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            // Simulate a response
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent($"Customer {customer.AuthId} deletion requested")
            };

            return Task.FromResult(response);
        }
    }
}