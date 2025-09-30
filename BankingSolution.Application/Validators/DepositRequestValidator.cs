using BankingSolution.Application.Dtos;
using FluentValidation;

namespace BankingSolution.Application.Validators;

public class DepositRequestValidator : AbstractValidator<DepositRequest>
{
    public DepositRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount should be greater that 0");
    }
}