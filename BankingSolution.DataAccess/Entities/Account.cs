namespace BankingSolution.DataAccess.Entities;

public class Account
{
    public int Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = [];
}