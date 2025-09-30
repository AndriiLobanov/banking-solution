namespace BankingSolution.Application.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string accountNumber, decimal requested, decimal balance)
        : base($"Insufficient funds on account {accountNumber}. Requested: {requested}, Balance: {balance}") { }
}