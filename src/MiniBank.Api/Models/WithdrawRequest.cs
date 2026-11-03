namespace MiniBank.Api.Models;

public record WithdrawRequest
{
    public decimal Amount { get; set; }
}