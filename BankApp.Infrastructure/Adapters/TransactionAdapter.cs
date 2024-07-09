using BankApp.Domain.Models;
using BankApp.Shared;

namespace BankApp.Infrastructure.Adapters
{
	public class TransactionAdapter : IAdapter<TransactionDto, Transaction>
	{
        public TransactionDto ToDto(Transaction source)
        {
            return new TransactionDto()
            {
                DestinationWalletId = source.DestinationWalletId.ToString(),
                SourceWalletId = source.SourceWalletId.ToString(),
                Message = source.Message,
                TransactionAmount = source.TransactionAmount
            };
        }

        public Transaction ToEntity(TransactionDto source)
        {
            return new Transaction()
            {
                DestinationWalletId = Guid.Parse(source.DestinationWalletId),
                SourceWalletId = Guid.Parse(source.SourceWalletId),
                Message = source.Message,
                TransactionAmount = source.TransactionAmount
            };
        }
    }
}

