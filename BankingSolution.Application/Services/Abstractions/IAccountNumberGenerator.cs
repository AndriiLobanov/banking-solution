namespace BankingSolution.Application.Services.Abstractions;

public interface IAccountNumberGenerator
{
    Task<string> GenerateUniqueAccountNumberAsync();
}
