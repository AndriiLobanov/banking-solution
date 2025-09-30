namespace BankingSolution.Application.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string accountNumber)
        : base($"Account with number {accountNumber} was not found.") { }
}