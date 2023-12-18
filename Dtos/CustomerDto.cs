namespace CustomerManagementService.Dtos;

public class CustomerDto
{
    public string AuthId { get; set; }
    public CustomerAccountDto CustomerAccountDto { get; set; }
    public CustomerProfileDto CustomerProfileDto { get; set; }
}