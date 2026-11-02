using MiniBank.Api.Models;
namespace MiniBank.Api.Services;

public interface IAccountService
{
    List<Account> GetAll();
}
