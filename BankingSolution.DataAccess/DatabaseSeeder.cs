using BankingSolution.DataAccess.Entities.Enums;
using BankingSolution.DataAccess.Entities;

namespace BankingSolution.DataAccess;

public static class DatabaseSeeder
{
    public static void SeedData(BankingDbContext context)
    {
        if (context.Accounts.Any())
        {
            return;
        }

        var accounts = new List<Account>
        {
            new Account
            {
                Id = 1,
                AccountNumber = "001",
                Name = "John Doe",
                Email = "john.doe@example.com",
                Balance = 5000.00m
            },
            new Account
            {
                Id = 2,
                AccountNumber = "002",
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Balance = 7500.50m
            },
            new Account
            {
                Id = 3,
                AccountNumber = "003",
                Name = "Robert Johnson",
                Email = "robert.johnson@example.com",
                Balance = 3200.75m
            },
            new Account
            {
                Id = 4,
                AccountNumber = "004",
                Name = "Emily Brown",
                Email = "emily.brown@example.com",
                Balance = 12000.00m
            },
            new Account
            {
                Id = 5,
                AccountNumber = "005",
                Name = "Michael Wilson",
                Email = "michael.wilson@example.com",
                Balance = 850.25m
            }
        };

        context.Accounts.AddRange(accounts);
        context.SaveChanges();

        var transactions = new List<Transaction>
        {
            new Transaction
            {
                Id = 1,
                Amount = 1000.00m,
                Timestamp = DateTime.UtcNow.AddDays(-10),
                FromAccount = null,
                ToAccount = "001",
                Type = TransactionType.Deposit,
                AccountId = 1
            },
            new Transaction
            {
                Id = 2,
                Amount = 2500.00m,
                Timestamp = DateTime.UtcNow.AddDays(-9),
                FromAccount = null,
                ToAccount = "002",
                Type = TransactionType.Deposit,
                AccountId = 2
            },
            new Transaction
            {
                Id = 3,
                Amount = 500.00m,
                Timestamp = DateTime.UtcNow.AddDays(-8),
                FromAccount = "001",
                ToAccount = null,
                Type = TransactionType.Withdraw,
                AccountId = 1
            },
            new Transaction
            {
                Id = 4,
                Amount = 300.00m,
                Timestamp = DateTime.UtcNow.AddDays(-7),
                FromAccount = "003",
                ToAccount = null,
                Type = TransactionType.Withdraw,
                AccountId = 3
            },
            new Transaction
            {
                Id = 5,
                Amount = 750.00m,
                Timestamp = DateTime.UtcNow.AddDays(-5),
                FromAccount = "001",
                ToAccount = "002",
                Type = TransactionType.Transfer,
                AccountId = 1
            },
            new Transaction
            {
                Id = 6,
                Amount = 1200.00m,
                Timestamp = DateTime.UtcNow.AddDays(-3),
                FromAccount = "004",
                ToAccount = "005",
                Type = TransactionType.Transfer,
                AccountId = 4
            },
            new Transaction
            {
                Id = 7,
                Amount = 450.50m,
                Timestamp = DateTime.UtcNow.AddDays(-2),
                FromAccount = "002",
                ToAccount = "003",
                Type = TransactionType.Transfer,
                AccountId = 2
            },
            new Transaction
            {
                Id = 8,
                Amount = 2000.00m,
                Timestamp = DateTime.UtcNow.AddDays(-1),
                FromAccount = null,
                ToAccount = "004",
                Type = TransactionType.Deposit,
                AccountId = 4
            }
        };

        context.Transactions.AddRange(transactions);
        context.SaveChanges();
    }
}