using BankingSolution.Application.Dtos;
using BankingSolution.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.WebApi.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        this.transactionService = transactionService;
    }

    [HttpPost("{accountNumber}/deposit")]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deposit([FromRoute] string accountNumber, [FromBody] DepositRequest request)
    {
        var result = await transactionService.DepositAsync(accountNumber, request);
        return Ok(result);
    }

    [HttpPost("{accountNumber}/withdraw")]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Withdraw([FromRoute] string accountNumber, [FromBody] WithdrawRequest request)
    {
        var result = await transactionService.WithdrawAsync(accountNumber, request);
        return Ok(result);
    }

    [HttpPost("transfer")]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        var result = await transactionService.TransferAsync(request);
        return Ok(result);
    }

    [HttpGet("{accountNumber}")]
    [ProducesResponseType(typeof(TransactionResponse[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTransactions([FromRoute] string accountNumber)
    {
        var transactions = await transactionService.GetAccountTransactionsAsync(accountNumber);
        return Ok(transactions);
    }
}