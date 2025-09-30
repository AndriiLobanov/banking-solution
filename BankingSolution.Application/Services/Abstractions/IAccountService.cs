using BankingSolution.Application.Dtos;

namespace BankingSolution.Application.Services.Abstractions;

public interface IAccountService
{
    Task<AccountResponse> CreateAccountAsync(CreateAccountRequest request);
    Task<AccountResponse?> GetAccountByAccountNumberAsync(string accountNumber);
    Task<IEnumerable<AccountResponse>> GetAllAccountsAsync();
}