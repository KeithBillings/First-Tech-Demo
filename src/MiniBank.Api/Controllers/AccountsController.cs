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

    [HttpPost("{id}/withdraw")]
    public ActionResult<Account> Withdraw(int id, WithdrawRequest request)
    {
        try
        {
            var updatedAccount = _accountService.Withdraw(id, request.Amount);
            return Ok(updatedAccount);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }
}
