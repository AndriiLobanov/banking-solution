using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSolution.Application.Services;

public abstract class BaseService
{
    private readonly IServiceProvider serviceProvider;

    protected BaseService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    protected async Task ValidateAsync<T>(T request, CancellationToken cancellationToken = default)
    {
        var validator = serviceProvider.GetService<IValidator<T>>();

        if (validator is not null)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}