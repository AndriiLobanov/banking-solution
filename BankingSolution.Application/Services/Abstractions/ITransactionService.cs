using BankingSolution.Application.Dtos;

namespace BankingSolution.Application.Services.Abstractions;

public interface ITransactionService
{
    Task<TransactionResponse> DepositAsync(string accountNumber, DepositRequest request);
    Task<TransactionResponse> WithdrawAsync(string accountNumber, WithdrawRequest request);
    Task<TransactionResponse> TransferAsync(TransferRequest request);
    Task<IEnumerable<TransactionResponse>> GetAccountTransactionsAsync(string accountNumber);
}