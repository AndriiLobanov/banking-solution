using BankingSolution.Application.Services;
using BankingSolution.Application.Services.Abstractions;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BankingSolution.Application.Extensions;

public static class ServiceCollectionsExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
        serviceCollection.AddSingleton(TypeAdapterConfig.GlobalSettings);
        serviceCollection.AddSingleton<IMapper, Mapper>();

        serviceCollection.AddValidatorsFromAssembly(assembly);

        serviceCollection.AddScoped<IAccountNumberGenerator, AccountNumberGenerator>();
        serviceCollection.AddScoped<ITransactionService, TransactionService>();
        serviceCollection.AddScoped<IAccountService, AccountService>();

        return serviceCollection;
    }
}