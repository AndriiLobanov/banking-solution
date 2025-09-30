using BankingSolution.Application.Dtos;
using FluentValidation;

namespace BankingSolution.Application.Validators;

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator()
    {
        RuleFor(x => x.InitialBalance)
            .GreaterThanOrEqualTo(0).WithMessage("Amount should not be less that 0");
    }
}