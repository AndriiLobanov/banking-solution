using BankingSolution.Application.Dtos;
using BankingSolution.DataAccess.Entities;
using Mapster;

namespace BankingSolution.Application.Mappings;

public class TransactionMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Transaction, TransactionResponse>()
            .Map(dst => dst.Amount, src => src.Amount)
            .Map(dst => dst.Timestamp, src => src.Timestamp)
            .Map(dst => dst.Type, src => src.Type.ToString())
            .Map(dst => dst.FromAccountNumber, src => src.FromAccount)
            .Map(dst => dst.ToAccountNumber, src => src.ToAccount);
    }
}