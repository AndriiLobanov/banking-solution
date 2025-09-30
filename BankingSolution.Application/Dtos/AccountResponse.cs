namespace BankingSolution.Application.Dtos;

public record AccountResponse(string AccountNumber, decimal Balance, string Name, string Email);