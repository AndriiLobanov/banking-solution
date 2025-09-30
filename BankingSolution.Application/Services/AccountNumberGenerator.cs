using BankingSolution.Application.Services.Abstractions;
using BankingSolution.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Application.Services;

public class AccountNumberGenerator : IAccountNumberGenerator
{
    private readonly BankingDbContext dbContext;
    private static readonly Random random = new();

    public AccountNumberGenerator(BankingDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<string> GenerateUniqueAccountNumberAsync()
    {
        string accountNumber;
        bool exists;

        do
        {
            accountNumber = GenerateAccountNumber();
            exists = await dbContext.Accounts
                .AnyAsync(a => a.AccountNumber == accountNumber);
        }
        while (exists);

        return accountNumber;
    }

    private string GenerateAccountNumber()
    {
        return string.Concat(Enumerable.Range(0, 3)
            .Select(_ => random.Next(0, 20).ToString()));
    }
}