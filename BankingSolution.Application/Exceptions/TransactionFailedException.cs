namespace BankingSolution.Application.Exceptions;

public class TransactionFailedException : Exception
{
    public TransactionFailedException(string message) : base(message) { }
}