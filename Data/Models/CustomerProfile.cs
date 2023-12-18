using System.ComponentModel.DataAnnotations;
using CustomerManagementService.Models;

namespace CustomerManagementService.Data.Models;

public class CustomerProfile
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string Town { get; set; }

    public string County { get; set; }

    public string Postcode { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public virtual Customer Customer { get; set; }
}