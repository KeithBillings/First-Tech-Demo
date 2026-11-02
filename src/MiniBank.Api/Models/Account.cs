namespace MiniBank.Api.Models;

public class Account
{
    public int Id { get; set; }
    public required string OwnerName { get; set; }
    public decimal Balance { get; set; }
}
