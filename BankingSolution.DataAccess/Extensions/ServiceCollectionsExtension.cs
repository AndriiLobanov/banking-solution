using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSolution.DataAccess.Extensions;

public static class ServiceCollectionsExtension
{
    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<BankingDbContext>(options =>
            options
                .UseInMemoryDatabase("BankingDb"));

        return serviceCollection;
    }
}