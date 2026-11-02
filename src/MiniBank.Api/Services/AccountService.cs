using MiniBank.Api.Models;

namespace MiniBank.Api.Services;

public class AccountService : IAccountService
{
    private readonly List<Account> _accounts =
    [
        new Account { Id = 1, OwnerName = "Maria Gonzalez",  Balance = 2450.75m },
        new Account { Id = 2, OwnerName = "James Okafor",    Balance = 18320.00m },
        new Account { Id = 3, OwnerName = "Priya Patel",     Balance = 640.12m },
        new Account { Id = 4, OwnerName = "David Chen",      Balance = 95.50m },
        new Account { Id = 5, OwnerName = "Sarah Whitfield", Balance = 7210.33m },
    ];

    public List<Account> GetAll()
    {
        return _accounts;
    }
}