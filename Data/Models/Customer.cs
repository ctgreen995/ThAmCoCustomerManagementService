using System.ComponentModel.DataAnnotations;
using CustomerManagementService.Data.Models;

namespace CustomerManagementService.Models;

public class Customer
{
    [Required]
    public Guid Id { get; set; }
    
    public string AuthId { get; set; }
    
    [Required]
    public virtual CustomerAccount CustomerAccount { get; set; }
    
    [Required]
    public virtual CustomerProfile CustomerProfile { get; set; }

}