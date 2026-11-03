using MiniBank.Api.Models;

namespace MiniBank.Api.Services;

public interface IAccountService
{
    List<Account> GetAll();

    Account Withdraw(int id, decimal amount);

}
