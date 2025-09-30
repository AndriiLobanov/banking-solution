using System.ComponentModel.DataAnnotations;

namespace BankingSolution.Application.Dtos;

public record CreateAccountRequest(
    [Required] decimal InitialBalance,
    [Required][EmailAddress] string Email,
    [Required][StringLength(100)] string Name);