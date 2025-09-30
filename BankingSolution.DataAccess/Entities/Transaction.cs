using BankingSolution.DataAccess.Entities.Enums;

namespace BankingSolution.DataAccess.Entities;

public class Transaction
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime Timestamp { get; set; }

    public string? FromAccount { get; set; }

    public string? ToAccount { get; set; }

    public TransactionType Type { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; }
}