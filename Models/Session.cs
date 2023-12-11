namespace CustomerDatabase.Models;

public class Session
{
    public string Id { get; set; }
    public string CustomerId { get; set; }
    public DateTime Date { get; set; }
    
    public string Location { get; set; }
    
    public string Length { get; set; }
    public Customer Customer { get; set; }
}