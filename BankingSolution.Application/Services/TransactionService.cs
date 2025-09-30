using BankingSolution.Application.Dtos;
using BankingSolution.Application.Exceptions;
using BankingSolution.Application.Services.Abstractions;
using BankingSolution.DataAccess;
using BankingSolution.DataAccess.Entities;
using BankingSolution.DataAccess.Entities.Enums;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Application.Services;

public class TransactionService : BaseService, ITransactionService
{
    private readonly BankingDbContext dbContext;
    private readonly IMapper mapper;

    public TransactionService(
        IServiceProvider serviceProvider,
        BankingDbContext dbContext,
        IMapper mapper) : base(serviceProvider)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<TransactionResponse> DepositAsync(string accountNumber, DepositRequest request)
    {
        await ValidateAsync(request);

        var account = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        if (account is null)
        {
            throw new AccountNotFoundException(accountNumber);
        }

        account.Balance += request.Amount;

        var transaction = new Transaction
        {
            AccountId = account.Id,
            Amount = request.Amount,
            Timestamp = DateTime.UtcNow,
            Type = TransactionType.Deposit,
            ToAccount = account.AccountNumber
        };

        dbContext.Transactions.Add(transaction);
        await dbContext.SaveChangesAsync();

        return mapper.Map<TransactionResponse>(transaction);
    }

    public async Task<TransactionResponse> WithdrawAsync(string accountNumber, WithdrawRequest request)
    {
        await ValidateAsync(request);

        var account = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        if (account is null)
        {
            throw new AccountNotFoundException(accountNumber);
        }

        if (account.Balance < request.Amount)
        {
            throw new InsufficientFundsException(accountNumber, request.Amount, account.Balance);
        }

        account.Balance -= request.Amount;

        var transaction = new Transaction
        {
            AccountId = account.Id,
            Amount = request.Amount,
            Timestamp = DateTime.UtcNow,
            Type = TransactionType.Withdraw,
            FromAccount = account.AccountNumber
        };

        dbContext.Transactions.Add(transaction);
        await dbContext.SaveChangesAsync();

        return mapper.Map<TransactionResponse>(transaction);
    }

    public async Task<TransactionResponse> TransferAsync(TransferRequest request)
    {
        await ValidateAsync(request);

        var fromAccount = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == request.FromAccountNumber);

        if (fromAccount is null)
        {
            throw new AccountNotFoundException(request.FromAccountNumber);
        }

        var toAccount = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == request.ToAccountNumber);

        if (toAccount is null)
        {
            throw new AccountNotFoundException(request.ToAccountNumber);
        }

        if (fromAccount.Balance < request.Amount)
        {
            throw new InsufficientFundsException(request.FromAccountNumber, request.Amount, fromAccount.Balance);
        }

        try
        {
            fromAccount.Balance -= request.Amount;
            toAccount.Balance += request.Amount;

            var transactionFromAccount = new Transaction
            {
                AccountId = fromAccount.Id,
                Amount = request.Amount,
                Timestamp = DateTime.UtcNow,
                Type = TransactionType.Transfer,
                FromAccount = fromAccount.AccountNumber,
                ToAccount = toAccount.AccountNumber
            };

            var transactionToAccount = new Transaction
            {
                AccountId = toAccount.Id,
                Amount = request.Amount,
                Timestamp = DateTime.UtcNow,
                Type = TransactionType.Transfer,
                FromAccount = fromAccount.AccountNumber,
                ToAccount = toAccount.AccountNumber
            };

            dbContext.Transactions.Add(transactionFromAccount);
            dbContext.Transactions.Add(transactionToAccount);
            await dbContext.SaveChangesAsync();


            return mapper.Map<TransactionResponse>(transactionFromAccount);
        }
        catch
        {
            throw new TransactionFailedException("Transfer failed due to an unexpected error.");
        }
    }

    public async Task<IEnumerable<TransactionResponse>> GetAccountTransactionsAsync(string accountNumber)
    {
        var account = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        if (account is null)
        {
            throw new AccountNotFoundException(accountNumber);
        }

        var transactions = await dbContext.Transactions
            .Where(t => t.AccountId == account.Id)
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync();

        return mapper.Map<TransactionResponse[]>(transactions);
    }
}