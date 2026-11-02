using Microsoft.AspNetCore.Mvc;
using MiniBank.Api.Models;
using MiniBank.Api.Services;

namespace MiniBank.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public ActionResult<List<Account>> GetAllAccounts()
    {
        var accounts = _accountService.GetAll();
        return Ok(accounts);
    }
}
