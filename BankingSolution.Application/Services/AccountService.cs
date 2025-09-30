using BankingSolution.Application.Dtos;
using BankingSolution.Application.Exceptions;
using BankingSolution.Application.Services.Abstractions;
using BankingSolution.DataAccess;
using BankingSolution.DataAccess.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Application.Services;

public class AccountService : BaseService, IAccountService
{
    private readonly BankingDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IAccountNumberGenerator accountNumberGenerator;

    public AccountService(
        IServiceProvider serviceProvider,
        BankingDbContext dbContext,
        IMapper mapper,
        IAccountNumberGenerator accountNumberGenerator) : base(serviceProvider)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.accountNumberGenerator = accountNumberGenerator;
    }

    public async Task<AccountResponse> CreateAccountAsync(CreateAccountRequest request)
    {
        await ValidateAsync(request);

        var account = mapper.Map<Account>(request);

        account.AccountNumber = await accountNumberGenerator.GenerateUniqueAccountNumberAsync();

        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();

        return mapper.Map<AccountResponse>(account);
    }

    public async Task<AccountResponse?> GetAccountByAccountNumberAsync(string accountNumber)
    {
        var account = await dbContext.Accounts
            .FirstOrDefaultAsync(x => x.AccountNumber.Equals(accountNumber));
        if (account is null)
        {
            throw new AccountNotFoundException(accountNumber);
        }

        var accountResponse = mapper.Map<AccountResponse>(account);

        return accountResponse;
    }

    public async Task<IEnumerable<AccountResponse>> GetAllAccountsAsync()
    {
        var accounts = await dbContext.Accounts.ToListAsync();

        var accountsResponse = mapper.Map<AccountResponse[]>(accounts);

        return accountsResponse;
    }
}