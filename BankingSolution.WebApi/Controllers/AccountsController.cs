using BankingSolution.Application.Dtos;
using BankingSolution.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.WebApi.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService accountService;

    public AccountsController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await accountService.CreateAccountAsync(request);
        return CreatedAtAction(nameof(GetAccount), new { accountNumber = result.AccountNumber }, result);
    }

    [HttpGet("{accountNumber}")]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAccount([FromRoute] string accountNumber)
    {
        var account = await accountService.GetAccountByAccountNumberAsync(accountNumber);

        return Ok(account);
    }

    [HttpGet]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAccounts()
    {
        var accounts = await accountService.GetAllAccountsAsync();
        return Ok(accounts);
    }
}