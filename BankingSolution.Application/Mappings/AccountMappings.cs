using BankingSolution.Application.Dtos;
using BankingSolution.DataAccess.Entities;
using Mapster;

namespace BankingSolution.Application.Mappings;

public class AccountMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Account, AccountResponse>()
            .Map(dst => dst.AccountNumber, src => src.AccountNumber)
            .Map(dst => dst.Email, src => src.Email)
            .Map(dst => dst.Name, src => src.Name)
            .Map(dst => dst.Balance, src => src.Balance);

        config.NewConfig<CreateAccountRequest, Account>()
            .Map(dst => dst.Balance, src => src.InitialBalance)
            .Map(dst => dst.Email, src => src.Email)
            .Map(dst => dst.Name, src => src.Name);
    }
}