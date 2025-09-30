namespace BankingSolution.Application.Dtos;

public record TransferRequest(string FromAccountNumber, string ToAccountNumber, decimal Amount);