namespace CustomerManagementService.Dtos;

public class CustomerDto
{
    public string AuthId { get; set; }
    public AccountDto Account { get; set; }
    public ProfileDto Profile { get; set; }
}