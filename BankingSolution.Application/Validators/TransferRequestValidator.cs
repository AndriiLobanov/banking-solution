using BankingSolution.Application.Dtos;
using FluentValidation;

namespace BankingSolution.Application.Validators;

public class TransferRequestValidator : AbstractValidator<TransferRequest>
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount of the transfer should be greater than 0");

        RuleFor(x => x)
            .Must(x => x.FromAccountNumber != x.ToAccountNumber)
            .WithMessage("Account numbers can not be identical")
            .WithName("FromAccount/ToAccount");
    }
}