using BankingSolution.Application.Dtos;
using FluentValidation;

namespace BankingSolution.Application.Validators;

public class WithdrawRequestValidator : AbstractValidator<WithdrawRequest>
{
    public WithdrawRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount should be greater that 0");
    }
}