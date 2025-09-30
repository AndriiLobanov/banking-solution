namespace BankingSolution.Application.Dtos;

public record TransactionResponse(int Id, string Type, decimal Amount, DateTime Timestamp, string? FromAccountNumber, string? ToAccountNumber);