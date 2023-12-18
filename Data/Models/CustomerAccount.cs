using CustomerManagementService.Models;

namespace CustomerManagementService.Data.Models;

public class CustomerAccount
{
    public Guid Id { get; set; }
    public Guid? CustomerId { get; set; }
    public double Funds { get; set; }
    
    public virtual Customer Customer { get; set; }
}