namespace Database.Models;

public class Customer
{
    public string Id { get; set; }
    public string Name { get; set; 
    public string Address { get; set; }
    public string Town { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public List<Order> Orders { get; set; }
}
